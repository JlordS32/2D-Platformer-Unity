using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    // Serializable fields
    [Header("Layers")]
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float gravityScale;

    [Header ("Extra jumps")]
    [SerializeField] private int extraJumps;
    private int jumpCounter;
    
    [Header ("Wall Jumping")]
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;

    [Header("Sound")]
    [SerializeField] private AudioClip jumpSound;

    [Header("Coyote Time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;

    // Private variables
    private Rigidbody2D body;
    private Animator animator;
    private BoxCollider2D boxCollider;
    private float horizontalInput;

    private void Awake()
    {
        // Grab references
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
        body.gravityScale = gravityScale;
    }

    private void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // Flip sprite over depending on horizontal direction.
        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // Set animator parameters
        animator.SetBool("run", horizontalInput != 0);
        animator.SetBool("grounded", isGrounded());

        // Jump 
        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        // Adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.linearVelocityY > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocityX, body.linearVelocityY / 2);
        }

        if (onWall())
        {
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = gravityScale;
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocityY);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; // Reset
                jumpCounter = extraJumps; // Reset
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }
    }

    private void Jump()
    { 
        if (coyoteCounter <= 0 && !onWall() && jumpCounter <= 0) return;

        SoundManager.instance.playSound(jumpSound);

        if (onWall()) {
            WallJump();
        } else {
            if (isGrounded()) {
                body.linearVelocity = new Vector2(body.linearVelocityX, jumpPower);
            } else {
                if (coyoteCounter > 0) {
                    body.linearVelocity = new Vector2(body.linearVelocityX, jumpPower);
                } else {
                    if (jumpCounter > 0) {
                        body.linearVelocity = new Vector2(body.linearVelocityX, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            coyoteCounter = 0;
        }
    }

    private void WallJump()
    {
        body.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x) * wallJumpX, wallJumpY));
    }

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }
    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}

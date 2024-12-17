using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [Header("Patrol Points")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;

    [Header("Movement Parameters")]
    [SerializeField] private float speed;
    [SerializeField] private float idleDuration;

    // Variables
    private Vector3 initScale;
    private bool movingLeft = true;
    private float idleTimer;
    private Animator animator;

    private void Awake()
    {
        initScale = transform.localScale;
        animator = GetComponent<Animator>();
    }

    private void OnDisable() {
        animator.SetBool("moving", false);
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (transform.position.x >= leftEdge.position.x)
                MoveInDirection(-1);
            else
                DirectionChange();
        }
        else
        {
            if (transform.position.x <= rightEdge.position.x)
                MoveInDirection(1);
            else
                DirectionChange();
        }
    }

    private void DirectionChange()
    {
        animator.SetBool("moving", false);
        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
            movingLeft = !movingLeft;
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        animator.SetBool("moving", true);

        transform.localScale = new Vector3(Mathf.Abs(initScale.x) * direction,
            initScale.y, initScale.z);

        transform.position = new Vector3(transform.position.x + Time.deltaTime * direction * speed,
            transform.position.y, transform.position.z);
    }
}

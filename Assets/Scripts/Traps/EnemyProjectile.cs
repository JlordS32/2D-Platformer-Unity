using UnityEngine;

public class EnemyProjectile : EnemyDamage
{
    // Serializable Fields
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;

    // Variables
    private float lifeTime;
    private bool hit;
    private Animator animator;
    private BoxCollider2D boxCollider;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActivateProjectile()
    {
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(true);
        boxCollider.enabled = true;
    }

    private void Update()
    {
        if (hit) return;

        float movementSpeed = speed * Time.deltaTime;
        transform.Translate(movementSpeed, 0, 0);

        // Lifetime 
        lifeTime += Time.deltaTime;
        if (lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);

        if (animator != null)
        {
            boxCollider.enabled = false;
            hit = true;
            animator.SetTrigger("explode");
            Debug.Log("triggered");
        }
        else gameObject.SetActive(false);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

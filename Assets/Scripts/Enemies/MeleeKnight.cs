using UnityEngine;

public class MeleeKnight : MonoBehaviour
{
    // Serialisables
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private float damage;
    [SerializeField] private BoxCollider2D boxCollider2D;
    [SerializeField] private LayerMask playerLayer;

    // Variables
    private float cooldownTimer = Mathf.Infinity;

    // References
    private Animator animator;
    private Health playerHealth;
    private EnemyPatrol enemyPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        if (PlayerInSight() && cooldownTimer >= attackCooldown)
        {
            cooldownTimer = 0;
            animator.SetTrigger("attack");
        }

        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        // Enemy position/origin
        Vector2 boxOrigin = boxCollider2D.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector2 boxSize = new Vector2(boxCollider2D.size.x * range, boxCollider2D.bounds.size.y);
        Vector2 boxDirection = Vector2.left;


        // Perform boxcast
        RaycastHit2D hit = Physics2D.BoxCast(boxOrigin, boxSize, 0, boxDirection, 0, playerLayer);

        if (hit.collider != null && hit.collider.tag == "Player")
        {
            playerHealth = hit.collider.GetComponent<Health>();
        }

        return hit.collider != null;
    }

    private void DamagePlayer()
    {
        if (PlayerInSight() && !playerHealth.isInvulnerable)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}

using UnityEngine;

public abstract class BaseKnight : MonoBehaviour
{
    // Serialisables
    [Header("Knight Parameters")]
    [SerializeField] protected float attackCooldown;
    [SerializeField] protected float range;
    [SerializeField] protected float damage;

    [Header("Colliders")]
    [SerializeField] protected float colliderDistance;
    [SerializeField] protected BoxCollider2D boxCollider2D;

    [Header("Layers")]
    [SerializeField] protected LayerMask playerLayer;

    // Variables
    protected float cooldownTimer = Mathf.Infinity;

    // References
    protected Animator animator;
    protected Health playerHealth;
    protected EnemyPatrol enemyPatrol;

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    protected abstract bool PlayerInSight();

    protected void DamagePlayer()
    {
        if (PlayerInSight() && !playerHealth.isInvulnerable)
        {
            playerHealth.TakeDamage(damage);
        }
    }
}
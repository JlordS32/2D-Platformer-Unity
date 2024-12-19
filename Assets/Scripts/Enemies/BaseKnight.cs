using UnityEngine;

public abstract class BaseKnight : MonoBehaviour
{
    // Serialisables
    [Header("Knight Parameters")]
    [SerializeField] protected float range;
    [SerializeField] protected float attackCooldown;

    [Header("Colliders")]
    [SerializeField] protected float colliderDistance;
    [SerializeField] protected BoxCollider2D boxCollider2D;

    [Header("Layers")]
    [SerializeField] protected LayerMask playerLayer;

    [Header ("Sounds")]
    [SerializeField] protected AudioClip attackSound;
    
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

    public void Deactivate() {
        gameObject.SetActive(false);
    }
}
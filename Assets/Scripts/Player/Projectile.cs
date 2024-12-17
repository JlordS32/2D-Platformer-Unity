using UnityEngine;
using UnityEngine.UIElements;

public class Projectile : MonoBehaviour
{
    // Serialisable Field
    [SerializeField] private float speed;
    [SerializeField] private float damage;

    // Variables
    private bool hit;
    private float _direction;
    private float _lifeTime;
    
    // References
    private BoxCollider2D boxCollider;
    private Animator animator;
    private Health enemyHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (hit)
        {
            return;
        }

        float movementSpeed = speed * Time.deltaTime * _direction;
        transform.Translate(movementSpeed, 0, 0);

        _lifeTime += Time.deltaTime;

        if (_lifeTime > 5) {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("explode");

        if (collision.tag == "Enemy") {
            enemyHealth = collision.GetComponent<Health>();
            enemyHealth?.TakeDamage(damage);
        }
    }

    public void setDirection(float direction)
    {
        _lifeTime = 0;
        _direction = direction;

        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }

        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactivate()
    {
        gameObject.SetActive(false);
    }
}

using System.Collections;
using UnityEngine;

public class Firetrap : MonoBehaviour
{
    [Header("Firetrap Timers")]
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;

    [Header("Firetrap Damage")]
    [SerializeField] private float damage;

    // Variables
    private Animator animator;
    private SpriteRenderer spriteRenderer;
    // private EnemyDamage enemyDamage;
    private bool triggered;
    private bool active;

    private Health playerHealth;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update() {
        if (playerHealth != null && active) {
            playerHealth.TakeDamage(damage);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerHealth = collision.GetComponent<Health>();

            if (!triggered)
            {
                StartCoroutine(ActivateFireTrap());
            }

            if (active)
            {
                playerHealth.TakeDamage(damage);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.tag == "Player") {
            playerHealth = null;
        }
    }

    private IEnumerator ActivateFireTrap()
    {

        triggered = true;
        spriteRenderer.color = Color.red;

        yield return new WaitForSeconds(activationDelay);
        spriteRenderer.color = Color.white;
        active = true;
        animator.SetBool("activated", true);

        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        animator.SetBool("activated", false);
    }
}

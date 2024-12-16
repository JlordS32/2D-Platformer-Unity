using UnityEngine;

public class Arrowtrap : MonoBehaviour
{
    // Serializable fields
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] arrows;
    
    // Variables
    private float cooldownTimer;

    private void Attack() {
        cooldownTimer = 0;

        arrows[FindArrows()].transform.position = firepoint.position;
        arrows[FindArrows()].GetComponent<EnemyProjectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindArrows() {
        for (int i = 0; i < arrows.Length; i++) {
            if (!arrows[i].activeInHierarchy) {
                return i;
            }
        }

        return 0;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (cooldownTimer >= attackCooldown) {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }
}

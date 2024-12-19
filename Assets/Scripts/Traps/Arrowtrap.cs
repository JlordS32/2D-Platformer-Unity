using UnityEngine;

public class Arrowtrap : MonoBehaviour
{
    // Serializable fields
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firepoint;
    [SerializeField] private GameObject[] arrows;
    [SerializeField] private AudioClip arrowSound;
    
    // Variables
    private float cooldownTimer;

    private void Attack() {
        cooldownTimer = 0;

        SoundManager.instance.playSound(arrowSound);
        arrows[FindArrows()].transform.position = firepoint.position;
        arrows[FindArrows()].GetComponent<EnemyProjectile>().ActivateProjectile();
    }

    private int FindArrows() {
        for (int i = 0; i < arrows.Length; i++) {
            if (!arrows[i].activeInHierarchy) {
                return i;
            }
        }

        return 0;
    }

    private void Update()
    {
        if (cooldownTimer >= attackCooldown) {
            Attack();
        }

        cooldownTimer += Time.deltaTime;
    }
}

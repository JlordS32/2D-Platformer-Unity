using UnityEngine;

public class EnemyDamage : MonoBehaviour
{
    [SerializeField] protected float _damage;

    virtual protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            collision.GetComponent<Health>().TakeDamage(_damage);
        }
    }
}

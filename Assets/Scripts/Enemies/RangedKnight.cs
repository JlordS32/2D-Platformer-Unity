using UnityEngine;

public class RangedKnight : BaseKnight
{
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

    protected override bool PlayerInSight()
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
}

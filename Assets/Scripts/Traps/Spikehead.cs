using Unity.VisualScripting;
using UnityEngine;

public class Spikehead : EnemyDamage
{
    [Header("Spikehead Properties")]
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask playerLayer;

    [Header("SFX")]
    [SerializeField] private AudioClip sound;
    
    private float checkTimer;
    private bool attacking;
    private Vector3 destination;
    private Vector3[] directions = new Vector3[4];

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * speed * Time.deltaTime);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer > checkDelay)
            {
                CheckForPlayer();
            }
        }
    }

    private void CheckForPlayer()
    {
        CalculateDirections();

        for (int i = 0; i < directions.Length; i++)
        {
            Debug.DrawRay(transform.position, directions[i], Color.red);
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directions[i], range, playerLayer);

            if (hit.collider != null && !attacking)
            {
                attacking = true;
                destination = directions[i];
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirections()
    {
        directions[0] = transform.right * range;    // Right direction
        directions[1] = -transform.right * range;   // Left direction
        directions[2] = transform.up * range;       // Up direction
        directions[3] = -transform.up * range;      // Down direction
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        SoundManager.instance.playSound(sound);
        base.OnTriggerEnter2D(collision);
        Stop();
    }
}

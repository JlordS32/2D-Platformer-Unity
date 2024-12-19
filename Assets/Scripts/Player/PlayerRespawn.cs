using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private AudioClip checkpointSound;
    private Transform currentCheckpoint;
    private Health playerHealth;

    private void Awake()
    {
        playerHealth = GetComponent<Health>();
    }

    public void CheckRespawn()
    {
        if (currentCheckpoint == null) {

            return;
        }

        transform.position = currentCheckpoint.position;
        playerHealth.Respawn();

        // Move cam to checkpont.
        Camera.main.GetComponent<CameraController>().MoveToNewRoom(currentCheckpoint.parent);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.transform.tag == "Checkpoint") {
            currentCheckpoint = collision.transform;
            SoundManager.instance.playSound(checkpointSound);
            collision.GetComponent<Collider2D>().enabled = false;
            collision.GetComponent<Animator>().SetTrigger("appear");
        }
    }
}

using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _previousRoom;
    [SerializeField] private Transform _nextRoom;
    [SerializeField] private CameraController _cameraController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") {
            if (collision.transform.position.x < transform.position.x) {
                _cameraController.MoveToNewRoom(_nextRoom);
            } else {
                _cameraController.MoveToNewRoom(_previousRoom);
            }
        }
    }
}

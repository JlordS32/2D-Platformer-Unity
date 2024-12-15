using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _previousRoom;
    [SerializeField] private Transform _nextRoom;
    [SerializeField] private CameraController _cameraController;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (_nextRoom == null) {
            Debug.LogError("Next room not set");
        }
        if (_previousRoom == null) {
            Debug.LogError("Previous room not set");
        }
        if (_cameraController == null) {
            Debug.LogError("Camera Controller not set!");
        }

        // if (collision.tag == "Player") {
        //     if (collision.transform.position.x < transform.position.x) {
        //         _cameraController.MoveToNewRoom(_nextRoom.localPosition.x);
        //         Debug.Log(_nextRoom.localPosition.x);
        //     } else {
        //         _cameraController.MoveToNewRoom(_previousRoom.localPosition.x);
        //         Debug.Log(_previousRoom.localPosition.x);
        //     }
        // }
    }
}

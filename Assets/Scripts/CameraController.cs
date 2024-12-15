using System.Net;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room Camera
    // [SerializeField] private float _speed; 
    // private float _currentPosX;
    // private Vector3 velocity = Vector3.zero;

    // Follow Player
    [SerializeField] private Transform _player;

    private void Update() {
        // Room Camera
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_currentPosX, transform.position.y, transform.position.z), ref velocity, _speed);

        // Follow player
        transform.position = new Vector3(_player.position.x, transform.position.y, transform.position.z);
    }

    // public void MoveToNewRoom(float newRoomPosX) {
    //     _currentPosX = newRoomPosX;
    // }
}

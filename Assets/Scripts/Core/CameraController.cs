using System.Net;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Room Camera
    [SerializeField] private float _speed; 
    private float _currentPosX;
    private Vector3 velocity = Vector3.zero;

    // Follow Player
    [SerializeField] private Transform _player;
    [SerializeField] private float _cameraOffset;
    [SerializeField] private float _cameraSpeed;
    private float _offset;

    private void Update() {
        // Room Camera
        // transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_currentPosX, transform.position.y, transform.position.z), ref velocity, _speed);

        // Follow player
        transform.position = new Vector3(_player.position.x + _offset, transform.position.y, transform.position.z);
        _offset = Mathf.Lerp(_offset, _cameraOffset * _player.localScale.x, Time.deltaTime * _cameraSpeed);
    }

    public void MoveToNewRoom(Transform newRoom) {
        _currentPosX = newRoom.position.x;
    }
}

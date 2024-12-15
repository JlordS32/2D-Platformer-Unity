using System.Net;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private float _speed; 
    private float _currentPosX;
    private Vector3 velocity = Vector3.zero;

    private void Update() {
        transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_currentPosX, transform.position.y, transform.position.z), ref velocity, _speed);
    }

    public void MoveToNewRoom(Transform _newRoom) {
        _currentPosX = _newRoom.position.x;
    }
}

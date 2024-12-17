using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform _previousRoom;
    [SerializeField] private Transform _nextRoom;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        
        if (collision.tag == "Player") {
            if (collision.transform.position.x < transform.position.x) {
                _nextRoom.GetComponent<Room>().ActivateRoom(true);
                _previousRoom.GetComponent<Room>().ActivateRoom(false);
                Debug.Log("Konichiwa");
            } else {
                _nextRoom.GetComponent<Room>().ActivateRoom(false);
                _previousRoom.GetComponent<Room>().ActivateRoom(true);
            }
        }
    }
}

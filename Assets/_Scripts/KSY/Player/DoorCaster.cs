using UnityEngine;
using UnityEngine.InputSystem;

using Ksy.Scripts.Object;

public class DoorCaster : MonoBehaviour
{
    private bool InDoor = false;
    private Door _door = null;
    void Update()
    {
        if(InDoor && Keyboard.current.fKey.wasPressedThisFrame && _door != null)
        {
            _door.Open(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Door door))
        {
            InDoor = true;
            this._door = door;
            Debug.Log($"{InDoor} , {_door?.gameObject.name}");
        }
    }
    void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Door door))
        {
            InDoor = false;
            _door = null;
            Debug.Log($"{InDoor} , {_door?.gameObject.name}");
        }
    }
}

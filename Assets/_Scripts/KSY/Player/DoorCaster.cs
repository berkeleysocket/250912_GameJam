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
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Door door))
        {
            InDoor = true;
            this._door = door;
            Debug.Log($"{InDoor} , {_door?.gameObject.name}");
        }        
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if(collision.gameObject.TryGetComponent(out Door door))
        {
            InDoor = false;
            _door = null;
            Debug.Log($"{InDoor} , {_door?.gameObject.name}");
        }
    }
}

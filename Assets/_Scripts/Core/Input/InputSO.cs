using System;
using UnityEngine;
using UnityEngine.InputSystem;
using static Controls;

namespace _Scripts.Core.Input
{
    [CreateAssetMenu(fileName = "InputSO", menuName = "SO/Input")]
    public class InputSO : ScriptableObject, IPlayerActions
    {
        public Controls Controls { get; private set; }
        public event Action<Vector2> OnMoved;
        public event Action<bool> OnSprinted;
        public event Action OnInteracted;
        public event Action OnInventoryed;

        public Vector2 MoveDir { get; private set; }

        private void OnEnable()
        {
            Controls ??= new Controls();
            
            Controls.Player.SetCallbacks(this);
            Controls.Enable();
        }
        
        private void OnDisable()
        {
            Controls.Disable();
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            MoveDir = context.ReadValue<Vector2>();
            OnMoved?.Invoke(MoveDir);
        }

        public void OnInteract(InputAction.CallbackContext context)
        {
            if (context.performed) OnInteracted?.Invoke();
        }

        public void OnSprint(InputAction.CallbackContext context)
        {
            if (context.performed) OnSprinted?.Invoke(true);
            if (context.canceled) OnSprinted?.Invoke(false);
        }

        public void OnInventory(InputAction.CallbackContext context)
        {
            if (context.performed) OnInventoryed?.Invoke();
        }
    }
}

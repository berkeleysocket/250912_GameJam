using System;
using _Scripts.Core.Utility;
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
        public event Action<int> OnNumbersPressed;
        public event Action OnInventoryed;
        public event Action OnInteracted;
        public event Action OnUsed;

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

        public void OnUse(InputAction.CallbackContext context)
        {
            if (context.performed) OnUsed?.Invoke();
        }

        public void OnOne(InputAction.CallbackContext context)
        {
            if (context.performed) OnNumbersPressed?.Invoke(1);
        }

        public void OnTwo(InputAction.CallbackContext context)
        {
            if (context.performed) OnNumbersPressed?.Invoke(2);
        }

        public void OnThree(InputAction.CallbackContext context)
        {
            if (context.performed) OnNumbersPressed?.Invoke(3);
        }

        public void OnFour(InputAction.CallbackContext context)
        {
            if (context.performed) OnNumbersPressed?.Invoke(4);
        }

        public void OnFive(InputAction.CallbackContext context)
        {
            if (context.performed) OnNumbersPressed?.Invoke(5);
        }
    }
}

using Ksy.Scripts.StressSystem;
using Ksy.Utility;
using Unity.VisualScripting.Dependencies.NCalc;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Ksy.Scripts.Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D body;
        [SerializeField] public float MaxSpeed
        {
            get
            {
                return _maxSpeed;
            }
            set
            {
                _maxSpeed = Mathf.Clamp(value,1,5);
            }
        }
        [SerializeField] private float _maxSpeed = 5f;
        [SerializeField] private float currentVelocity = 0f;
        public NotifyValue<bool> Notify_IsMove {get; private set;} = new NotifyValue<bool>();
        public NotifyValue<Vector2> Notify_Dir {get; private set;} = new NotifyValue<Vector2>();

        private float _acceleration = 50;
        private float _deacceleration = 50;


        #region UnityEvent
        private void Awake()
        {
            if(body == null)
                body = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            currentVelocity = CalculateSpeed(Notify_Dir.Value);

            if(Keyboard.current.spaceKey.wasPressedThisFrame) StressManager.Instance?.IncreaseStress(1);
            if(Keyboard.current.gKey.wasPressedThisFrame) StressManager.Instance?.DecreaseStress(1);
        }
        private void FixedUpdate()
        {
            if(body != null)
                body.linearVelocity = (Notify_Dir.Value * currentVelocity);
        }
        #endregion
        public void Move(Vector2 dir)
        {
            Notify_Dir.Value = dir;

            if(dir != Vector2.zero)
                Notify_IsMove.Value = true;
            else Notify_IsMove.Value = false;
        }
        public void MoveHorizontal(float xDir)
        {
            Notify_Dir.Value = new Vector2(xDir, Notify_Dir.Value.y);
        }
        public void MoveVertical(float yDir)
        {
            Notify_Dir.Value = new Vector2(Notify_Dir.Value.x, yDir);
        }
        private float CalculateSpeed(Vector2 inputDir)
        {
            if(inputDir.sqrMagnitude > 0)
            {
                currentVelocity += _acceleration * Time.deltaTime / 1.5f;
            }
            else
            {
                currentVelocity -= _deacceleration * Time.deltaTime / 1.5f;
            }

            return Mathf.Clamp(currentVelocity, 0, MaxSpeed);
        }
    }
}


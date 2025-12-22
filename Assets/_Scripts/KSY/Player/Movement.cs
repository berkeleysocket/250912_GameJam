using Ksy.Utility;
using UnityEngine;

namespace Ksy.Scripts.Player
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float _maxSpeed = 5f;
        public NotifyValue<float> Notify_CurrentVelocity {get; private set;} = new NotifyValue<float>();
        public NotifyValue<Vector2> Notify_Dir {get; private set;} = new NotifyValue<Vector2>();
        private Rigidbody2D _body;

        private float _acceleration = 50;
        private float _deacceleration = 50;


        #region UnityEvent
        private void Awake()
        {
            _body = GetComponent<Rigidbody2D>();
        }
        private void Update()
        {
            Notify_CurrentVelocity.Value = CalculateSpeed(Notify_Dir.Value);
        }
        private void FixedUpdate()
        {
            _body.linearVelocity = (Notify_Dir.Value * Notify_CurrentVelocity.Value);
        }
        #endregion
        public void Move(Vector2 dir)
        {
            Notify_Dir.Value = dir;
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
                Notify_CurrentVelocity.Value += _acceleration * Time.deltaTime / 1.5f;
            }
            else
            {
                Notify_CurrentVelocity.Value -= _deacceleration * Time.deltaTime / 1.5f;
            }

            return Mathf.Clamp(Notify_CurrentVelocity.Value, 0, _maxSpeed);
        }
    }
}


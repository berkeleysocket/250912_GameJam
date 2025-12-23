using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

using Ksy.Scripts.StressSystem;
using Ksy.Utility;

namespace Ksy.Scripts._Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private float currentSpeed = 5f;
        [SerializeField] private float maxSpeed = 15f;
        [SerializeField] private float currentVelocity = 0f;
        public float CurrentSpeed
        {
            get
            {
                return currentSpeed;
            }
            set
            {
                currentSpeed = Mathf.Clamp(value,1,maxSpeed);
            }
        }

        public NotifyValue<bool> notify_IsMove {get; private set;} = new NotifyValue<bool>();
        public NotifyValue<Vector2> notify_Dir {get; private set;} = new NotifyValue<Vector2>();

        //private List<Coroutine> _movementBuffs = new List<Coroutine>();
        private Rigidbody2D _body;
        private float _acceleration = 50;
        private float _deacceleration = 50;


        #region UnityEvent
        private void Awake()
        {
            if(!gameObject.TryGetComponent(out _body)) _body = gameObject.AddComponent<Rigidbody2D>();
        }
        private void Update()
        {
            currentVelocity = CalculateSpeed(notify_Dir.Value);

            if(Keyboard.current.spaceKey.wasPressedThisFrame) StressManager.Instance?.IncreaseStress(1);
            if(Keyboard.current.fKey.wasPressedThisFrame) StressManager.Instance?.DecreaseStress(1);
            if(Keyboard.current.eKey.wasPressedThisFrame) SpeedUp(1f,3f);
        }
        private void FixedUpdate()
        {
            if(_body != null)
                _body.linearVelocity = (notify_Dir.Value * currentVelocity);
        }
        #endregion
        public void Move(Vector2 dir)
        {
            notify_Dir.Value = dir;

            if(dir != Vector2.zero)
                notify_IsMove.Value = true;
            else notify_IsMove.Value = false;
        }
        public void SpeedUp(float speed, float duration)
        {
            if(this.currentSpeed >= maxSpeed) return;

            var buff = StartCoroutine(_speedUp(speed,duration));
        }
        private IEnumerator _speedUp(float speed, float duration)
        {
            CurrentSpeed += speed;

            yield return new WaitForSeconds(duration);

            CurrentSpeed -= speed;
        }
        private float CalculateSpeed(Vector2 inputDir)
        {
            if(inputDir.sqrMagnitude > 0)
            {
                currentVelocity += _acceleration * Time.deltaTime / 1.3f;
            }
            else
            {
                currentVelocity -= _deacceleration * Time.deltaTime / 1.3f;
            }

            return Mathf.Clamp(currentVelocity, 0, CurrentSpeed);
        }
    }
}


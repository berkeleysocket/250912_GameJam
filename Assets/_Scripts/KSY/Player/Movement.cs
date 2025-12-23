using System.Collections;
using UnityEngine;

using Ksy.Utility;
using _Scripts.Core.Structs;
using _Scripts.Core.Events;

namespace Ksy.Scripts._Player
{
    public class Movement : MonoBehaviour
    {
        [SerializeField] private SpeedDataEventChannel speedDataEventChannel;
        [SerializeField] private float currentSpeed = 5f;
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
        public float maxSpeed = 15f;
        public NotifyValue<bool> notify_IsMove {get; private set;} = new NotifyValue<bool>();
        public NotifyValue<Vector2> notify_Dir {get; private set;} = new NotifyValue<Vector2>();

        private Rigidbody2D _body;
        private float _acceleration = 50;
        private float _deacceleration = 50;


        #region UnityEvent
        private void Awake()
        {
            if(!gameObject.TryGetComponent(out _body)) _body = gameObject.AddComponent<Rigidbody2D>();
            speedDataEventChannel.OnEvent += SpeedUp;
        }
        private void Update()
        {
            currentVelocity = CalculateSpeed(notify_Dir.Value);
        }
        private void FixedUpdate()
        {
            if(_body != null)
                _body.linearVelocity = (notify_Dir.Value * currentVelocity);
        }
        private void OnDestroy()
        {
            speedDataEventChannel.OnEvent -= SpeedUp;
        }
        #endregion
        public void Move(Vector2 dir)
        {
            notify_Dir.Value = dir;

            if(dir != Vector2.zero)
                notify_IsMove.Value = true;
            else notify_IsMove.Value = false;
        }
        public void SpeedUp(SpeedData speedData)
        {
            if(this.currentSpeed >= maxSpeed) return;

            var buff = StartCoroutine(_speedUp(speedData));
        }
        private IEnumerator _speedUp(SpeedData speedData)
        {
            CurrentSpeed += speedData.Speed;

            yield return new WaitForSeconds(speedData.Duration);

            CurrentSpeed -= speedData.Speed;
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

            return Mathf.Clamp(currentVelocity, 0, maxSpeed);
        }
    }
}


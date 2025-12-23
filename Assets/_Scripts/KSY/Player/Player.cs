using UnityEngine;

using _Scripts.Core.Input;
using Ksy.Scripts.StressSystem;
using _Scripts.Core.Events;

namespace Ksy.Scripts._Player
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public InputSO InputEvent {get; private set;}
        [field: SerializeField] public Movement MovementCompo {get; private set;}
        [field: SerializeField] public RendererX RednererCompo {get; private set;}
        [field: SerializeField] public AnimationX AnimationCompo {get; private set;}
        [field: SerializeField] public AudioSource AudioSourceCompo {get; private set;}
        [SerializeField] private EmptyEventChannel gameOverEventChannel;
        [SerializeField] private Vector3EventChannel traceEventChannel; 
        [SerializeField] private GameObject tracePrefab;
        private void Awake()
        {
            traceEventChannel.OnEvent += Trace;
        }

        void Start()
        {
            if(InputEvent != null && MovementCompo != null)
            {
                InputEvent.OnMoved += MovementCompo.Move;
            }
            if(MovementCompo != null && RednererCompo != null)
            {
                MovementCompo.notify_Dir.OnChangedValue += RednererCompo.FilpX;
            }
            if(MovementCompo != null && AnimationCompo != null)
            {
                MovementCompo.notify_IsMove.OnChangedValue += AnimationCompo.SetIsMove;
                MovementCompo.notify_Dir.OnChangedValue += AnimationCompo.SetMoveDir;
            }
            if(MovementCompo != null && AudioSourceCompo != null)
            {
                MovementCompo.notify_IsMove.OnChangedValue += WalkSound;
            }
            if(StressManager.Instance != null)
            {
                StressManager.Instance.StressIncreased += (args)=> MovementCompo.maxSpeed -= 1;
                StressManager.Instance.StressDecreased += (args)=> MovementCompo.maxSpeed += 1;
            }
        }

        void OnDisable()
        {
            if(InputEvent != null && MovementCompo != null)
            {
                InputEvent.OnMoved -= MovementCompo.Move;
            }
            if(MovementCompo != null && RednererCompo != null)
            {
                MovementCompo.notify_Dir.OnChangedValue -= RednererCompo.FilpX;
            }
            if(MovementCompo != null && AnimationCompo != null)
            {
                MovementCompo.notify_IsMove.OnChangedValue -= AnimationCompo.SetIsMove;
                MovementCompo.notify_Dir.OnChangedValue -= AnimationCompo.SetMoveDir;
            }
            traceEventChannel.OnEvent -= Trace;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {     
            if (collision.CompareTag("Director"))
            {
                gameOverEventChannel.Raise(new());
            }
        }

        public void WalkSound(bool isMove)
        {
            if(isMove)
                AudioSourceCompo.Play();
            else
                AudioSourceCompo.Stop();
        }

        private void Trace(Vector3 vector3)
        {
            var trace = Instantiate(tracePrefab);
            trace.transform.position = vector3;
        }
    }
}



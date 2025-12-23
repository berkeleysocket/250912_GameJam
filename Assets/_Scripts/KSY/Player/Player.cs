using UnityEngine;

using _Scripts.Core.Input;
using Ksy.Scripts.StressSystem;
using JetBrains.Annotations;
using _Scripts.Core.Events;

namespace Ksy.Scripts._Player
{
    public class Player : MonoBehaviour
    {
        public static int keyCount = 0;
        [field: SerializeField] public InputSO InputEvent {get; private set;}
        [field: SerializeField] public Movement MovementCompo {get; private set;}
        [field: SerializeField] public RendererX RednererCompo {get; private set;}
        [field: SerializeField] public AnimationX AnimationCompo {get; private set;}
        [SerializeField] private EmptyEventChannel gameOverEventChannel;

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
            StressManager.Instance.StressIncreased += (args)=> MovementCompo.CurrentSpeed -= args.applyValue;
            StressManager.Instance.StressDecreased += (args)=> MovementCompo.CurrentSpeed += args.applyValue;
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
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Director"))
            {
                gameOverEventChannel.Raise(new());
            }
        }
    }
}



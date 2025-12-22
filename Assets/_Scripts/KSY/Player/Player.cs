using UnityEngine;

using _Scripts.Core.Input;
using Ksy.Scripts.StressSystem;

namespace Ksy.Scripts.Player
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public InputSO InputEvent {get; private set;}
        [field: SerializeField] public Movement MovementCompo {get; private set;}
        [field: SerializeField] public RendererX RednererCompo {get; private set;}
        [field: SerializeField] public AnimationX AnimationCompo {get; private set;}

        void Start()
        {
            if(InputEvent != null && MovementCompo != null)
            {
                InputEvent.OnMoved += MovementCompo.Move;
            }
            if(MovementCompo != null && RednererCompo != null)
            {
                MovementCompo.Notify_Dir.OnChangedValue += RednererCompo.FilpX;
            }
            if(MovementCompo != null && AnimationCompo != null)
            {
                MovementCompo.Notify_IsMove.OnChangedValue += AnimationCompo.SetIsMove;
                MovementCompo.Notify_Dir.OnChangedValue += AnimationCompo.SetMoveDir;
            }
            StressManager.Instance.StressIncreased += (args)=> MovementCompo.MaxSpeed -= args.applyValue;
            StressManager.Instance.StressDecreased += (args)=> MovementCompo.MaxSpeed += args.applyValue;

            StressManager.Instance.StressIncreased += (args)=> MovementCompo.MaxSpeed -= args.applyValue;
        }

        void OnDisable()
        {
            if(InputEvent != null && MovementCompo != null)
            {
                InputEvent.OnMoved -= MovementCompo.Move;
            }
            if(MovementCompo != null && RednererCompo != null)
            {
                MovementCompo.Notify_Dir.OnChangedValue -= RednererCompo.FilpX;
            }
            if(MovementCompo != null && AnimationCompo != null)
            {
                MovementCompo.Notify_IsMove.OnChangedValue -= AnimationCompo.SetIsMove;
                MovementCompo.Notify_Dir.OnChangedValue -= AnimationCompo.SetMoveDir;
            }
        }
    }
}



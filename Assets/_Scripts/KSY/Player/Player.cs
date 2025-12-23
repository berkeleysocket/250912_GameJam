using UnityEngine;

using _Scripts.Core.Input;
using Ksy.Scripts.StressSystem;

namespace Ksy.Scripts._Player
{
    public class Player : MonoBehaviour
    {
        [field: SerializeField] public InputSO InputEvent {get; private set;}
        [field: SerializeField] public Movement MovementCompo {get; private set;}
        [field: SerializeField] public RendererX RednererCompo {get; private set;}
        [field: SerializeField] public AnimationX AnimationCompo {get; private set;}
        [field: SerializeField] public AudioSource AudioSourceCompo {get; private set;}

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

        public void WalkSound(bool isMove)
        {
            if(isMove)
                AudioSourceCompo.Play();
            else
                AudioSourceCompo.Stop();
        }
    }
}



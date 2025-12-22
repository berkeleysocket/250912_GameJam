using AJ._01.Scripts.FSM.States;
using UnityEngine;

namespace AJ._01.Scripts.FSM
{
    [RequireComponent(typeof(Director))]
    public class DirectorBrain : MonoBehaviour
    {
        private DirectorStateMachine _stateMachine;
        private Director _director;

        private void Awake()
        {
            _director = GetComponent<Director>();
            _stateMachine = new DirectorStateMachine();
        
            _stateMachine.AddState(DirectorStateType.Idle, new DirectorIdleState(_director, "Idle", _stateMachine));
            _stateMachine.AddState(DirectorStateType.Chase, new DirectorChaseState(_director, "Chase", _stateMachine));
            _stateMachine.AddState(DirectorStateType.Looking, new DirectorLookingState(_director, "Looking", _stateMachine));
        }
        private void Start()
        {
            _stateMachine.Initialized(DirectorStateType.Chase);
        }
        private void Update()
        {
            _stateMachine.CurrentState.Update();
        }
    }
}

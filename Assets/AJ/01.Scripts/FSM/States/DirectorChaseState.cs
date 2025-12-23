using _Scripts.Core.Utility;
using UnityEngine;

namespace AJ._01.Scripts.FSM.States
{
    public class DirectorChaseState : DirectorState
    {
        public DirectorChaseState(Director director, string animName, DirectorStateMachine stateMachine) : base(director, animName, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            Logging.Log("Chase");
            Director.SetMove(true);
        }
        public override void Update()
        {
            base.Update();
            if (Director.AgentCompo.remainingDistance <= Director.AgentCompo.stoppingDistance)
            {
                Logging.Log("Idle");
                StateMachine.ChangeState(DirectorStateType.Idle);
                return;
            }
            
            Director.UpdateAgentTarget();
            UpdateAnimationBasedOnVelocity();
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
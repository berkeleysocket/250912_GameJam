using _Scripts.Core.Utility;
using UnityEngine;

namespace AJ._01.Scripts.FSM.States
{
    public class DirectorChaseState : DirectorState
    {
        private float distance;
        public DirectorChaseState(Director director, string animName, DirectorStateMachine stateMachine) : base(director, animName, stateMachine)
        {
            distance = director.AgentCompo.stoppingDistance;
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
            Director.AgentCompo.stoppingDistance = distance;
            base.Exit();
        }
    }
}
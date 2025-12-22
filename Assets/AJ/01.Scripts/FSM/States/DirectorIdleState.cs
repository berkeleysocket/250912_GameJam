using _Scripts.Core.Utility;
using UnityEngine;

namespace AJ._01.Scripts.FSM.States
{
    public class DirectorIdleState : DirectorState
    {
        public DirectorIdleState(Director director, string animName, DirectorStateMachine stateMachine) : base(director, animName, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            Logging.Log("Idle");
            Director.SetMove(false);
            Director.UpdateAgentTarget();
        }
        public override void Update()
        {
            base.Update();
            UpdateAnimationBasedOnVelocity();
            float dist = Vector2.Distance(Director.transform.position, Director.Target.position);

            if (dist > Director.AgentCompo.stoppingDistance)
            {
                Director.UpdateAgentTarget();
                StateMachine.ChangeState(DirectorStateType.Chase);
                return;
            }
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}
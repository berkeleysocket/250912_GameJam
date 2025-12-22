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
            Debug.Log("isOnNavMesh : " + Director.AgentCompo.isOnNavMesh);
            Debug.Log("hasPath : " + Director.AgentCompo.hasPath);
            Debug.Log("pathPending : " + Director.AgentCompo.pathPending);
            Debug.Log("isStopped : " + Director.AgentCompo.isStopped);
            Debug.Log("destination : " + Director.AgentCompo.destination);

            if (Director.Target.position.sqrMagnitude > Director.AgentCompo.stoppingDistance)
            {
                Director.SetMove(true);
                Director.UpdateAgentTarget();
                Logging.Log("ChangeChaseState");
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
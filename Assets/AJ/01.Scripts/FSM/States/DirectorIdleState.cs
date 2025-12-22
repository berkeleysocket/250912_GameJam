using _Scripts.Core.Utility;

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
            Director.SetMove(true);
        }
        public override void Update()
        {
            base.Update();
            if (Director.Target != null && Director.CanMove)
            {
                StateMachine.ChangeState(DirectorStateType.Chase);
            }
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}
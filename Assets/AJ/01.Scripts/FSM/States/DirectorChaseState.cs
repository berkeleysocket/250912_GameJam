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
            Director.SetMove(true);
        }
        public override void Update()
        {
            if (Director.Target == null || !Director.CanMove)
            {
                StateMachine.ChangeState(DirectorStateType.Idle);
                return;
            }
            Director.UpdateAgentTarget();
        }
        public override void Exit()
        {
            
        }
    }
}
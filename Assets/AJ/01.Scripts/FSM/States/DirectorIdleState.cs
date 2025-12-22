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
            Director.SetMove(true);
        }
        public override void Update()
        {
            base.Update();
            Director.UpdateAgentTarget();
        }
        public override void Exit()
        {
            base.Exit();
        }
    }
}
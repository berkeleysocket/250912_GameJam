namespace AJ._01.Scripts.FSM.States
{
    public class DirectorLookingState : DirectorState
    {
        private DirectorLooking _directorLooking;
        public DirectorLookingState(Director director, string animName, DirectorStateMachine stateMachine) : base(director, animName, stateMachine)
        {
            _directorLooking = director.GetComponent<DirectorLooking>();
        }
        public override void Enter()
        {
            base.Enter();
        }
        public override void Update()
        {
            Director.SetMove(false);
            if(_directorLooking.IsAnimationEnd)
                StateMachine.ChangeState(DirectorStateType.Chase);
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}
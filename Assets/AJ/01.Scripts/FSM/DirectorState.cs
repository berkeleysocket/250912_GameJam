using UnityEngine;

namespace AJ._01.Scripts.FSM
{
    public abstract class DirectorState
    {
        protected readonly Director Director;
        protected readonly DirectorStateMachine StateMachine;
        protected readonly int AnimHash;
        public DirectorState(Director director, string animName, DirectorStateMachine stateMachine)
        {
            Director = director;
            StateMachine = stateMachine;
            AnimHash = Animator.StringToHash(animName);
        }
        public virtual void Enter()
        {   
            Director.AnimCompo.SetBool(AnimHash, true);
        }

        public virtual void Update()
        {
            if (Director.bossCall)
                Director.HandleChangeTarget(Director.BossCallTransform);
            else
                Director.HandleChangeTarget(Director.Player);
        }
        public virtual void Exit()
        {
            Director.AnimCompo.SetBool(AnimHash, false);
        }
    }
}

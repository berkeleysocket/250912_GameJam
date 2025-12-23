using _Scripts.Core.Utility;
using UnityEngine;

namespace AJ._01.Scripts.FSM
{
    public abstract class DirectorState
    {
        protected readonly Director Director;
        protected readonly DirectorStateMachine StateMachine;
        protected readonly int AnimHash;
        private static readonly int MoveXHash = Animator.StringToHash("MoveX");
        private static readonly int MoveYHash = Animator.StringToHash("MoveY");
        private float _timer;
        public DirectorState(Director director, string animName, DirectorStateMachine stateMachine)
        {
            Director = director;
            StateMachine = stateMachine;
            AnimHash = Animator.StringToHash(animName);
        }
        public virtual void Enter()
        {   
            Director.AnimCompo.SetBool(AnimHash, true);
            
            Director.AnimCompo.SetFloat(MoveXHash, 0f);
            Director.AnimCompo.SetFloat(MoveYHash, 0f);
            _timer = 0f;
        }

        public virtual void Update()
        {
            if (Director.FindPlayer)
                StateMachine.ChangeState(DirectorStateType.Looking);
            if (Director.bossCall)
            {
                Logging.Log("BossCall");
                Director.SetMove(true);
                Director.HandleChangeTarget(Director.BossCallTransform);
                Director.UpdateAgentTarget();
                if (Director.AgentCompo.remainingDistance <= Director.AgentCompo.stoppingDistance)
                {
                    Logging.Log("Boss Call");
                    _timer += Time.deltaTime;
                    if (_timer > Director.bossCallTime)
                    {
                        Director.HandleChangeTarget(Director.Player);
                        Director.bossCall = false;
                    }
                }
            }
            else
            {
                Director.HandleChangeTarget(Director.Player);
            }
        }
        

        protected void UpdateAnimationBasedOnVelocity()
        {
            var agent = Director.AgentCompo;
            if (agent == null) return;

            Vector2 v = agent.velocity;

            /*if (v.sqrMagnitude < 0.0001f)
            {
                Director.AnimCompo.SetFloat(MoveXHash, 0f);
                Director.AnimCompo.SetFloat(MoveYHash, 0f);
                return;
            }*/

            Vector2 dir = v.normalized;

            float x = dir.x;
            float y = dir.y;

            Director.AnimCompo.SetFloat(MoveXHash, x);
            Director.AnimCompo.SetFloat(MoveYHash, y);
        }
        public virtual void Exit()
        {
            Director.AnimCompo.SetFloat(MoveXHash, 0f);
            Director.AnimCompo.SetFloat(MoveYHash, 0f);
            Director.AnimCompo.SetBool(AnimHash, false);
        }
    }
}

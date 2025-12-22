using _Scripts.Core.Utility;
using UnityEngine;

namespace AJ._01.Scripts.FSM.States
{
    public class DirectorChaseState : DirectorState
    {
        private static readonly int MoveXHash = Animator.StringToHash("MoveX");
        private static readonly int MoveYHash = Animator.StringToHash("MoveY");

        private readonly DirectorRenderer _renderer;
        public DirectorChaseState(Director director, string animName, DirectorStateMachine stateMachine) : base(director, animName, stateMachine)
        {
            
        }

        public override void Enter()
        {
            base.Enter();
            Logging.Log("Enter");
            Director.AnimCompo.SetFloat(MoveXHash, 0f);
            Director.AnimCompo.SetFloat(MoveYHash, 0f);
        }
        public override void Update()
        {
            base.Update();
            Director.UpdateAgentTarget();

            var agent = Director.AgentCompo;
            if (agent == null) return;

            Vector2 v = agent.velocity;

            if (v.sqrMagnitude < 0.0001f)
            {
                Director.AnimCompo.SetFloat(MoveXHash, 0f);
                Director.AnimCompo.SetFloat(MoveYHash, 0f);
                return;
            }

            Vector2 dir = v.normalized;

            float x = dir.x;
            float y = dir.y;

            Director.AnimCompo.SetFloat(MoveXHash, x);
            Director.AnimCompo.SetFloat(MoveYHash, y);

            if (_renderer != null)
                _renderer.Flip(v);
        }
        public override void Exit()
        {
            Director.AnimCompo.SetFloat(MoveXHash, 0f);
            Director.AnimCompo.SetFloat(MoveYHash, 0f);
            base.Exit();
        }
    }
}
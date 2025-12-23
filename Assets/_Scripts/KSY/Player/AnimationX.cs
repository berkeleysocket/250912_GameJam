using UnityEngine;

namespace Ksy.Scripts._Player
{
    public class AnimationX : MonoBehaviour
    {
        private Animator _animator;
        private readonly int _hash_IsMove = Animator.StringToHash("IsMove");
        private readonly int _hash_MoveDirX = Animator.StringToHash("MoveHorizontal");
        private readonly int _hash_MoveDirY = Animator.StringToHash("MoveDirY");        

        #region UnityEvent
        void Awake()
        {
            _animator = GetComponent<Animator>();
        }
        #endregion
        public void SetIsMove(bool value)
        {
            _animator.SetBool(_hash_IsMove,value);
        }
        public void SetMoveDir(Vector2 dir)
        {
            if(dir == Vector2.zero) return;
            _animator.SetFloat(_hash_MoveDirX, Mathf.Abs(dir.x));
            _animator.SetFloat(_hash_MoveDirY, dir.y);
        }
    }
}


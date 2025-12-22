using UnityEngine;

namespace Ksy.Scripts.Player
{
    public class AnimationX : MonoBehaviour
    {
        private Animator _animator;
        private readonly int _hash_Velocity = Animator.StringToHash("Velocity");
        private readonly int _hash_FlipY = Animator.StringToHash("FlipY");

        public void SetVelocityParm(float value)
        {
            _animator.SetFloat(_hash_Velocity,value);
        }

        public void SetDirParm(Vector2 value)
        {
            //_animator.SetBool(_hash_FlipY,value);
        }
    }
}


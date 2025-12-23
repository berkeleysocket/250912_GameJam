using UnityEngine;

namespace AJ._01.Scripts.FSM
{
    public class DirectorLooking : MonoBehaviour
    {
        private DirectorAnimator _animator;
        private Director _director;
        private FieldOfView _fov;
        private LayerMask _evidenceMask;
        public bool IsAnimationEnd { get; set; }
        private void Awake()
        {
            _director = GetComponent<Director>();
            _animator = GetComponentInChildren<DirectorAnimator>();
            _fov = GetComponentInChildren<FieldOfView>();
            _evidenceMask = _fov.evidence;
        }

        private void Start()
        {
            _animator.OnTraceStart += FindPlayer;
            _animator.OnTraceEnd += () =>
            {
                _director.FindPlayer = false;
                IsAnimationEnd = true;
            };
        }

        private void FindPlayer()
        {
            IsAnimationEnd = false;
            /*_fov.evidence = LayerMask.NameToLayer("Player");
            var d = _fov.TryGetEvidenceInFov(out Transform s);
            if (d)
                _director.Target = s;*/
        }
    }
}
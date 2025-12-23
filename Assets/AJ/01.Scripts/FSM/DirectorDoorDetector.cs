using Ksy.Scripts.Object;
using UnityEngine;

namespace AJ._01.Scripts.FSM
{
    public class DirectorDoorDetector : MonoBehaviour
    {
        private Director _director;

        [Header("Door Detection Settings")] 
        [SerializeField]private float detectionDistance = 2f;
        [SerializeField] private float detectionRadius = 0.5f;
        [SerializeField] private LayerMask doorLayer;
        private void Awake()
        {
            _director = GetComponent<Director>();
        }
        private void Update()
        {
            if (_director == null || _director.AgentCompo == null) return;
            if (!_director.CanMove) return;
            
            CheckForDoorsInPath();
        }
        private void CheckForDoorsInPath()
        {
            Vector2 moveDirection = _director.AgentCompo.velocity.normalized;
            
            if (moveDirection.sqrMagnitude < 0.01f) return;

            RaycastHit2D hit = Physics2D.CircleCast(transform.position, detectionRadius, moveDirection, detectionDistance, doorLayer);

            if (hit.collider != null)
            {
                Door door = hit.collider.GetComponent<Door>();
                if (door != null && !door.IsOpen)
                {
                    door.Reactive();
                    Debug.Log($"Door opened: {door.name}");
                }
            }
        }
        private void OnDrawGizmos()
        {
            if (_director == null || _director.AgentCompo == null) return;
            
            Vector2 moveDirection = _director.AgentCompo.velocity.normalized;
            if (moveDirection.sqrMagnitude < 0.01f) return;

            Gizmos.color = Color.yellow;
            Vector2 start = transform.position;
            Vector2 end = start + moveDirection * detectionDistance;
            
            Gizmos.DrawLine(start, end);
            Gizmos.DrawWireSphere(end, detectionRadius);
        }
    }
}
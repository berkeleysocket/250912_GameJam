using System;
using _Scripts.Core.Events;
using _Scripts.Core.Utility;
using AJ._01.Scripts.FSM;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AJ._01.Scripts
{
    [AddComponentMenu("Director/Director")]
    public class Director : MonoBehaviour 
    {
        [field:SerializeField] public Transform Target { get; private set; }
        [SerializeField] private float speed;
        [SerializeField] private bool canMove = true;
        [SerializeField] private ChangeTargetEventChannel changeTargetEventChannel;
        public Transform Player { get; private set; }
        public float Speed
        {
            get => speed;
            set
            {
                speed = value;
                AgentCompo.speed = speed;
            }
        }

        public bool CanMove => canMove;
        public NavMeshAgent AgentCompo { get; private set; }
        public Animator AnimCompo { get; private set; }  
        public DirectorRenderer RendererCompo { get; private set; }
        private void Awake()
        {
            AgentCompo = GetComponent<NavMeshAgent>();
            AnimCompo = GetComponentInChildren<Animator>();
            RendererCompo = GetComponentInChildren<DirectorRenderer>();
            Player = Target;
            if(AgentCompo != null)
            {
                AgentCompo.speed = speed;
                AgentCompo.updateRotation = false;
                AgentCompo.updateUpAxis = false;
            }
        }


        private void OnEnable()
        {
            if (changeTargetEventChannel != null)
                changeTargetEventChannel.OnEvent += HandleChangeTarget;
        }

        private void OnDisable()
        {
            if (changeTargetEventChannel != null)
                changeTargetEventChannel.OnEvent -= HandleChangeTarget;
        }

        private void HandleChangeTarget(Transform t)
        {
            Target = t;
        }
        private void Update()
        {
            if (AgentCompo != null)
                AgentCompo.isStopped = !canMove;
        }

        private void LateUpdate()
        {
            RendererCompo.Flip((AgentCompo.steeringTarget - transform.position).normalized);
        }

        public void SetMove(bool value)
        {
            canMove = value;
            if (AgentCompo != null) AgentCompo.isStopped = !canMove;
        }
        public void UpdateAgentTarget()
        {
            if (Target == null) return;
            AgentCompo.SetDestination(Target.position);
        }
    }
}

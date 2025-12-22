using System;
using _Scripts.Core.Events;
using _Scripts.Core.Structs;
using _Scripts.Core.Utility;
using AJ._01.Scripts.FSM;
using Ksy.Scripts.TraceSystem;
using NUnit.Framework.Constraints;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Serialization;

namespace AJ._01.Scripts
{
    public class Director : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public Transform Target
        {
            get
            {
                if (target == null) return Player;

                return target;
            }
            set => target = value;
        }
        [SerializeField] private float speed;
        [SerializeField] private bool canMove = true;
        [SerializeField] private ChangeTargetEventChannel changeTargetEventChannel;
        public Transform Player { get; private set; }
        [field:SerializeField]public Transform BossCallTransform { get; set; }
        public bool bossCall = false;
        public TraceChannel traceChannel;
        [field:SerializeField]public bool FindPlayer { get; set; } 
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
            if(traceChannel != null)
                traceChannel.OnEvent += HandleFind;
        }

        private void HandleFind(Empty b)
        {
            FindPlayer = true;
        }

        private void OnDisable()
        {
            if (changeTargetEventChannel != null)
                changeTargetEventChannel.OnEvent -= HandleChangeTarget;
            if(traceChannel != null)
                traceChannel.OnEvent -= HandleFind;
        }
    
        private void Update()
        {
            if (AgentCompo != null)
                AgentCompo.isStopped = !canMove;
        }
        public void HandleChangeTarget(Transform t)
        {
            Target = t;
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
            if (Target == null) 
            {
                Target = Player;
                return;
            }
            AgentCompo.SetDestination(Target.position);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Trace trace))
            {
                trace.Interaction(gameObject);
            }
        }
    }
}

using System;
using _Scripts.Core.Events;
using UnityEngine;
using UnityEngine.AI;

namespace AJ._01.Scripts
{
    public class Director : MonoBehaviour 
    {
        [field:SerializeField] public Transform Target { get; private set; }
        [field:SerializeField] public NavMeshAgent AgentCompo { get; private set; }
        [SerializeField] private float speed;
        [SerializeField] private bool canMove = true;
        [SerializeField] private ChangeTargetEventChannel emptyEventChannel;
        public float Speed
        {
            get => speed;
            set
            {
                speed = value;
                AgentCompo.speed = speed;
            }
        }

        private void Awake()
        {
            AgentCompo.speed = speed;   
            AgentCompo.updateRotation = false;
            AgentCompo.updateUpAxis = false;
            emptyEventChannel.OnEvent += (t) => Target = t;
        }
        private void Update()
        {
            AgentCompo.isStopped = !canMove;

            UpdateAgentTarget();
        }

        private void UpdateAgentTarget()
        {
            AgentCompo.SetDestination(Target.position);
        }
    }
}

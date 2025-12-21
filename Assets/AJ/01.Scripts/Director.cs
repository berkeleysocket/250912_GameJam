using System;
using UnityEngine;
using UnityEngine.AI;

namespace AJ._01.Scripts
{
    public class Director : MonoBehaviour
    {
        [field:SerializeField] public Transform Target { get; private set; }
        [field:SerializeField] public NavMeshAgent AgentCompo { get; private set; }

        [SerializeField] private bool canMove = true;
        [SerializeField] private float speed;
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
        }

        private void Update()
        {
            if (canMove)
                AgentCompo.SetDestination(Target.position);
        }
    }
}

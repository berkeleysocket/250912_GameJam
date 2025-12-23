using _Scripts.Core.Events;
using _Scripts.Core.Structs;
using AJ._01.Scripts.FSM;
using Ksy.Scripts.TraceSystem;
using Unity.Collections;
using UnityEngine;
using UnityEngine.AI;

namespace AJ._01.Scripts
{
    public class Director : MonoBehaviour
    {
        [SerializeField] private Transform target;
        public Transform Target
        {
            get
            {
                if (!IsValid(target))
                {
                    if(FieldOfview == null) GetComponentInChildren<FieldOfView>();
                    FieldOfview.TryGetEvidenceInFov(out Transform t);
                    return t != null ? t : Player;
                }

                return target;
            }
            set => target = value;
        }
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
        [SerializeField] private bool canMove = true;
        public Transform Player { get; private set; }
        [field:SerializeField]public Transform BossCallTransform { get; set; }
        public bool bossCall = false;
        public float bossCallTime = 10f;
        public TraceChannel traceChannel;
        [field:SerializeField]public bool FindPlayer { get; set; } 
        
        [field:SerializeField] public FieldOfView FieldOfview { get; private set; }
        [SerializeField] private Vector2 velocity;
        [SerializeField] private EmptyEventChannel directorSpeedUpEventChannel;
        [SerializeField] private EmptyEventChannel directorSpeedDownEventChannel;
        
        public AudioSource AudioCompo { get; private set; }
        public bool CanMove => canMove;
        public NavMeshAgent AgentCompo { get; private set; }
        public Animator AnimCompo { get; private set; }  
        public DirectorRenderer RendererCompo { get; private set; }
        
        [SerializeField] private Vector2 velocity;
        private void Awake()
        {
            AgentCompo = GetComponent<NavMeshAgent>();
            AudioCompo = GetComponent<AudioSource>();
            AnimCompo = GetComponentInChildren<Animator>();
            RendererCompo = GetComponentInChildren<DirectorRenderer>();
            FieldOfview = GetComponentInChildren<FieldOfView>();
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
            FieldOfview.onTargetInFov += HandleChangeTarget;
            directorSpeedUpEventChannel.OnEvent += SpeedUp;
            directorSpeedDownEventChannel.OnEvent += SpeedDown;
            if(traceChannel != null)
                traceChannel.OnEvent += HandleFind;
        }

        private void OnDisable()
        {
            FieldOfview.onTargetInFov -= HandleChangeTarget;
            directorSpeedUpEventChannel.OnEvent -= SpeedUp;
            directorSpeedDownEventChannel.OnEvent -= SpeedDown;
            if(traceChannel != null)
                traceChannel.OnEvent -= HandleFind;
        }
        public void SpeedUp(Empty empty)
        {
            Speed += 1;
        }

        public void SpeedDown(Empty empty)
        {
            Speed -= 1;
        }

        private void HandleFind(Empty b)
        {
            FindPlayer = true;
        }

    
        private void Update()
        {
            if (AgentCompo != null)
                AgentCompo.isStopped = !canMove;
            velocity = AgentCompo.velocity;
        }
        private bool IsValid(Transform t)
        {
            return t != null && t.gameObject != null;
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

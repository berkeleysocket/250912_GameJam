using UnityEngine;
using System;

using Ksy.Scripts.TraceSystem;

namespace Ksy.Scripts.Object
{
    public class Door : MonoBehaviour, ITraceReactive
    {
        private Animator animator;
        [SerializeField] private Dir openDir;
        private BoxCollider2D OpenColider;
        private SpriteRenderer FrameRenderer;
        

        private readonly int _hash_Open = Animator.StringToHash("IsOpen");
        public event Action OnOpend;
        public event Action OnClosed;



        public bool IsOpen {get; private set;}

        void Awake()
        {
            animator = GetComponent<Animator>();
            OpenColider = GetComponent<BoxCollider2D>();
            FrameRenderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        public void Open()
        {
            IsOpen = true;
            if(OpenColider != null)
                OpenColider.isTrigger = true;
            OnOpend?.Invoke();
        }
        public void Close()
        {
            IsOpen = false;
            if(OpenColider != null)
                OpenColider.isTrigger = false;
            OnClosed?.Invoke();
        }

        public GameObject GetGameObject() => gameObject; 

        [ContextMenu("Reactive")]
        public void Reactive()
        {
            animator?.SetBool(_hash_Open,true);
            Open();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(OpenColider.isTrigger && !collision.isTrigger)
            {
                // Debug.Log($"{collision.name}");
                FrameRenderer.sortingOrder = 11;
            }
        }
        void OnTriggerExit2D(Collider2D collision)
        {
            if(OpenColider.isTrigger && !collision.isTrigger)
            {
                // Debug.Log($"{collision.name}");
                FrameRenderer.sortingOrder = 0;
            }
        }
    }
}


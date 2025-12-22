using UnityEngine;
using System;

using Ksy.Scripts.TraceSystem;

namespace Ksy.Scripts.Object
{
    public class Door : MonoBehaviour, ITraceReactive
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Dir openDir;
        [SerializeField] private BoxCollider2D OpenColider;
        [SerializeField] private SpriteRenderer FrameRenderer;
        

        private readonly int _hash_Open = Animator.StringToHash("IsOpen");
        public event Action OnOpend;
        public event Action OnClosed;



        public bool IsOpen {get; private set;}

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
        [ContextMenu("Reactive")]
        public void Reactive()
        {
            animator?.SetBool(_hash_Open,true);
            Open();
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(OpenColider.isTrigger)
            {
                // Debug.Log($"{collision.name}");
                FrameRenderer.sortingOrder = 11;
            }
        }
        void OnTriggerExit2D(Collider2D collision)
        {
            if(OpenColider.isTrigger)
            {
                // Debug.Log($"{collision.name}");
                FrameRenderer.sortingOrder = 0;
            }
        }
    }
}


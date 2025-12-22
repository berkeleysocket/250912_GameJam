using UnityEngine;
using System;

using Ksy.Scripts.TraceSystem;

namespace Ksy.Scripts.Object
{
    public class Door : MonoBehaviour, ITraceReactive
    {
        [SerializeField] private Animator animator;
        [SerializeField] private Dir openDir;
        [SerializeField] private BoxCollider2D sheet;

        private readonly int _hash_Open = Animator.StringToHash("IsOpen");
        public event Action OnOpend;
        public event Action OnClosed;



        public bool IsOpen {get; private set;}

        public void Open()
        {
            IsOpen = true;
            if(sheet != null)
                sheet.isTrigger = true;
            OnOpend?.Invoke();
        }
        public void Close()
        {
            IsOpen = false;
            if(sheet != null)
                sheet.isTrigger = false;
            OnClosed?.Invoke();
        }
        [ContextMenu("Reactive")]
        public void Reactive()
        {
            animator?.SetBool(_hash_Open,true);
            Open();
        }
    }
}


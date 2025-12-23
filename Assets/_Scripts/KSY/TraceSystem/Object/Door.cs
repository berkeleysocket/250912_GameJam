using UnityEngine;
using System;
using _Scripts.Core.Structs;
using AJ._01.Scripts;
using Ksy.Scripts.TraceSystem;

namespace Ksy.Scripts.Object
{
    public class Door : MonoBehaviour, ITraceReactive
    {
        [SerializeField] private bool NeedKey = false;
        [SerializeField] private bool NeedBoss = false;
        private Animator _animator;
        private BoxCollider2D _colider;
        private SpriteRenderer _frameReanderer;
        public bool IsOpen {get; private set;}
        private readonly int _hash_Open = Animator.StringToHash("IsOpen");
        public event Action OnOpend;
        public event Action OnClosed;

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _colider = GetComponent<BoxCollider2D>();
            _frameReanderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        public void Open(GameObject actor)
        {
            IsOpen = true;
            if(_colider != null)
                _colider.isTrigger = true;
            OnOpend?.Invoke();
        }
        public void Close()
        {
            IsOpen = false;
            if(_colider != null)
                _colider.isTrigger = false;
            OnClosed?.Invoke();
        }

        public GameObject GetGameObject() => gameObject; 

        [ContextMenu("Reactive")]
        public void Reactive(GameObject reactor)
        {
            _animator?.SetBool(_hash_Open,true);
            Open(reactor);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(_colider.isTrigger && !collision.isTrigger)
            {
                _frameReanderer.sortingOrder = 11;
            }
        }
        void OnTriggerExit2D(Collider2D collision)
        {
            if(_colider.isTrigger && !collision.isTrigger)
            {
                _frameReanderer.sortingOrder = 0;
            }
        }
    }
}


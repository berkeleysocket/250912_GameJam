using UnityEngine;
using System;

using _Scripts.Core.Structs;
using AJ._01.Scripts;
using Ksy.Scripts.TraceSystem;
using Ksy.Scripts._Player;
using UnityEngine.Events;

namespace Ksy.Scripts.Object
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private bool NeedKey = false;
        [SerializeField] private bool NeedDirector = false;
        private Animator _animator;
        private BoxCollider2D _colider;
        private SpriteRenderer _frameReanderer;
        public bool IsOpen {get; private set;}
        private readonly int _hash_Open = Animator.StringToHash("IsOpen");
        public event Action OnOpend;
        public event Action<string> OnFailedOpen;
            

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _colider = GetComponent<BoxCollider2D>();
            _frameReanderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();
        }

        public void Open(GameObject actor)
        {
            if(actor != null && actor.tag == "Player")
            {
                if(NeedKey)
                {
                    if(Player.keyCount <= 0) 
                    {
                        OnFailedOpen?.Invoke("열쇠가 필요한 문입니다.");
                        return;
                    }
                }
                else if(NeedDirector)
                {
                    OnFailedOpen?.Invoke("잠긴 문입니다. 다른 누군가가 열 수 있을지도..");
                    return;
                }
            }
            _animator?.SetBool(_hash_Open,true);

            IsOpen = true;
            if(_colider != null)
                _colider.isTrigger = true;
            OnOpend?.Invoke();
        }

        public GameObject GetGameObject() => gameObject; 

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


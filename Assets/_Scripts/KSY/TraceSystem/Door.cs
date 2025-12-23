using UnityEngine;
using System;

using _Scripts.YTH.Alert;
using _Scripts.Core.Events;
using _Scripts.YTH.Inventory;

namespace Ksy.Scripts.Object
{
    public class Door : MonoBehaviour
    {
        [SerializeField] private bool NeedKey = false;
        [SerializeField] private bool NeedDirector = false;
        [SerializeField] private AlertDataEventChannel alertDataEventChannel;
        [SerializeField] private ItemDataEventChannel itemCanRemoveEventChannel;
        [SerializeField] private BoolEventChannel InventoryUpdateEventChannel;
        [SerializeField] private ItemDataEventChannel itemRemoveEventChannel;
        [SerializeField] private ItemDataSO keyItem;
        [SerializeField] private AudioSource openAudio;
        [SerializeField] private AudioSource failedopenAudio;
        private Animator _animator;
        private BoxCollider2D _colider;
        private SpriteRenderer _frameReanderer;
        private bool _canOpen;
        public bool IsOpen {get; private set;}
        private readonly int _hash_Open = Animator.StringToHash("IsOpen");
        public event Action OnOpend;
        public event Action OnFailedOpen;

            

        void Awake()
        {
            _animator = GetComponent<Animator>();
            _colider = GetComponent<BoxCollider2D>();
            _frameReanderer = gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>();

            if(openAudio != null &&  openAudio.clip != null)
            {
                OnOpend += openAudio.Play;
            }
            if(failedopenAudio != null && failedopenAudio.clip != null)
            {
                OnFailedOpen += failedopenAudio.Play;
            }
        }

        public void Open(GameObject actor)
        {
            if(IsOpen) return;
            if(actor != null && actor.tag == "Player")
            {
                if(NeedKey)
                {
                    Check();
                    if(_canOpen)
                    {
                        itemRemoveEventChannel.Raise(new ItemData(keyItem.ItemID));
                    }
                    else
                    {
                        OnFailedOpen?.Invoke();
                        alertDataEventChannel.Raise(new AlertData("- 잠긴 문입니다. -", "열쇠가 필요합니다.", 2.5f, 0.5f));
                        return;
                    }
                }
                else if(NeedDirector)
                {
                    OnFailedOpen?.Invoke();
                    alertDataEventChannel.Raise(new AlertData("- 잠긴 문입니다. -", "다른 누군가가 열 수 있을지도...", 2.5f, 0.5f));
                    return;
                }
            }

            if(_colider != null)
            {
                _animator?.SetBool(_hash_Open,true);
                IsOpen = true;
                _colider.isTrigger = true;
                OnOpend?.Invoke();
            }
            else
            {
                OnFailedOpen?.Invoke();
            }
        }
        
        public void Check()
        {
            InventoryUpdateEventChannel.OnEvent += OnCheck;
            itemCanRemoveEventChannel.Raise(new ItemData(keyItem.ItemID));
        }

        private void OnCheck(bool active)
        {
            _canOpen = active;
            InventoryUpdateEventChannel.OnEvent -= OnCheck;
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


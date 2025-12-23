using System;
using UnityEngine;

using _Scripts.Core.Events;
using _Scripts.YTH.Inventory;
using _Scripts.YTH.Alert;
using UnityEngine.SceneManagement;

namespace Ksy.Scripts
{
    public class Clear : MonoBehaviour
    {
        [SerializeField] private AlertDataEventChannel alertDataEventChannel;
        [SerializeField] private ItemDataEventChannel itemCanRemoveEventChannel;
        [SerializeField] private BoolEventChannel InventoryUpdateEventChannel;
        [SerializeField] private ItemDataEventChannel itemRemoveEventChannel;
        [SerializeField] private ItemDataSO keyItem;
        [SerializeField] private IntEventChannel currentTranslationEventChannel;
        [SerializeField] private GameObject transition;

        private bool _canClear;
        private int _translation = 0;

        public event Action OnOpend;

        private void Awake()
        {
            currentTranslationEventChannel.OnEvent += SetTranslation;
        }

        private void OnDestroy()
        {
            currentTranslationEventChannel.OnEvent -= SetTranslation;
        }

        public bool TryClear()
        {
            Check();
            if(_canClear)
            {
                if (_translation >= 7)
                {
                    itemRemoveEventChannel.Raise(new ItemData(keyItem.ItemID));
                    return true;
                }
            }

            return false;
        }
        public void Check()
        {
            InventoryUpdateEventChannel.OnEvent += OnCheck;
            itemCanRemoveEventChannel.Raise(new ItemData(keyItem.ItemID));
        }
        private void OnCheck(bool active)
        {
            _canClear = active;
            InventoryUpdateEventChannel.OnEvent -= OnCheck;
        }
        void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.tag == "Player")
            {
                if(TryClear())
                {
                    //Show Clear UI
                    transition.SetActive(true);
                }
                else
                {
                    //Show Clear Condition UI
                    alertDataEventChannel.Raise(new AlertData("- 퇴근할 수 없습니다! -", "\"업무를 모두 완료하고 열쇠를 찾아 다시오세요.\"", 2.5f, 0.5f));
                }
            }
        }

        private void SetTranslation(int count)
        {
            _translation = count;
        }

    }
}


using _Scripts.Core.Events;
using _Scripts.Core.Input;
using _Scripts.YTH.Inventory;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.YTH.Object
{    
    public class VendingMachine : MonoBehaviour
    {
        [Header("Vending Machine Settings")]
        [SerializeField] private ItemDataSO resultItem;
        [SerializeField] private ItemDataSO costItem;
        [SerializeField] private GameObject keyPrompt;
        [SerializeField] private InputSO inputSO;

        [Header("Event Channels")]
        [SerializeField] private ItemDataEventChannel canRemoveItemEventChannel;
        [SerializeField] private ItemDataEventChannel removeItemEventChannel;
        [SerializeField] private ItemDataEventChannel canAddItemEventChannel;
        [SerializeField] private ItemDataEventChannel addItemEventChannel;
        [SerializeField] private BoolEventChannel inventoryUpdateEventChannel;
        [SerializeField] private AlertDataEventChannel alertDataEventChannel;

        private bool m_CanBuy = false;
        private bool m_CanSell = false;
        private bool m_playerinRange = false;

        private void Awake()
        {
            keyPrompt.transform.localScale = Vector3.zero;
            keyPrompt.SetActive(false);
            m_playerinRange = false;
            m_CanSell = false;
            m_CanBuy = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (keyPrompt != null)
                {
                    Sequence seq = DOTween.Sequence();
                    keyPrompt.transform.DOKill();
                    keyPrompt.transform.localScale = Vector3.zero;
                    seq.AppendCallback(() => keyPrompt.SetActive(true));
                    seq.Append(keyPrompt.transform.DOScale(1f, 0.2f).SetEase(Ease.OutCubic));
                    alertDataEventChannel.Raise(new($"- {resultItem.ItemName} 자판기 -", $"{costItem.ItemName}으로 구매 가능합니다.", 2.5f, 0.25f));
                }
                m_playerinRange = true;
                inputSO.OnInteracted += Use;
            }            
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (keyPrompt != null)
                {
                    Sequence seq = DOTween.Sequence();
                    keyPrompt.transform.DOKill();
                    seq.Append(keyPrompt.transform.DOScale(0f, 0.2f).SetEase(Ease.OutCubic));
                    seq.AppendCallback(() => keyPrompt.SetActive(false));
                }
                m_playerinRange = false;
                inputSO.OnInteracted -= Use;
            }
        }

        public void Use()
        {
            if (m_playerinRange)
            {    
                Remove();
                Add();
                if (m_CanBuy && m_CanSell)
                {
                    removeItemEventChannel.Raise(new ItemData(costItem.ItemID));
                    addItemEventChannel.Raise(new ItemData(resultItem.ItemID));
                    alertDataEventChannel.Raise(new($"- 아이템을 구매했습니다 -", $"{resultItem.ItemName}", 2.5f, 0.25f));
                }
            }
        }

        public void Remove()
        {
            inventoryUpdateEventChannel.OnEvent += OnRemove;
            canRemoveItemEventChannel.Raise(new ItemData(costItem.ItemID));
        }

        private void OnRemove(bool active)
        {
            m_CanSell = active;
            inventoryUpdateEventChannel.OnEvent -= OnRemove;
        }

        public void Add()
        {
            inventoryUpdateEventChannel.OnEvent += OnAdd;
            canAddItemEventChannel.Raise(new ItemData(resultItem.ItemID));
        }

        private void OnAdd(bool active)
        {
            m_CanBuy = active;
            inventoryUpdateEventChannel.OnEvent -= OnAdd;
        }
    }
}

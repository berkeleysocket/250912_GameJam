using System.Collections.Generic;
using System.Linq;
using _Scripts.Core.Events;
using _Scripts.Core.Input;
using _Scripts.Core.Utility;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.Core.YTH.Inventory
{    
    public class InventoryManager : MonoBehaviour
    {
        public bool IsEmpty => m_inventorySlots.Count == 0;
        public RectTransform Rect => m_rectTransform ??= inventory.GetComponent<RectTransform>();

        [Header("Inventory Settings")]
        [SerializeField] private InventoryItem prefab;
        [SerializeField] private InputSO inputSO;
        [SerializeField] private GameObject inventory;

        [Header("Event Channel")]
        [SerializeField] private ItemDataEventChannel itemAddEventChannel;
        [SerializeField] private ItemDataEventChannel itemRemoveEventChannel;
        [SerializeField] private InventoryManagerEventChannel inventoryManagerEventChannel;
        [SerializeField] private BoolEventChannel CanAddItemEventChannel;
        [SerializeField] private BoolEventChannel CanRemoveItemEventChannel;
        [SerializeField] private BoolEventChannel InventoryOpenEventChannel;

        private List<InventorySlot> m_inventorySlots;
        private bool m_acvite;
        private RectTransform m_rectTransform;
    

        private void Awake()
        {
            m_inventorySlots = GetComponentsInChildren<InventorySlot>().ToList();

            itemAddEventChannel.OnEvent += TryAddItem;
            itemRemoveEventChannel.OnEvent += TryRemoveItem;
            inputSO.OnInventoryed += OnInventory;
        }

        private void Start()
        {
            foreach (var slot in m_inventorySlots)
            {
                slot.Initialize(this);
            }
            
            inventoryManagerEventChannel.Raise(this);
        }

        private void OnDestroy()
        {
            itemAddEventChannel.OnEvent -= TryAddItem;
            itemRemoveEventChannel.OnEvent -= TryRemoveItem;
            inputSO.OnInventoryed -= OnInventory;
        }

        public void CanAddItem(ItemData item)
        {
            foreach (var slot in m_inventorySlots)
            {
                if (slot.InventoryItem == null)
                {
                    CanAddItemEventChannel.Raise(true);
                    return;
                }
            }

            CanAddItemEventChannel.Raise(false);
            return;   
        }

        public void CanRemoveItem(ItemData item)
        {
            if (IsEmpty) return;

            var itemDataSO = ItemDatabase.GetItemDataSO(item.itemID);

            foreach (var slot in m_inventorySlots)
            {
                if (slot.InventoryItem.Item == itemDataSO)
                {
                    CanRemoveItemEventChannel.Raise(true);
                    return;
                }
            }

            CanRemoveItemEventChannel.Raise(false);
            return;
        }

        public void TryAddItem(ItemData item)
        {
            var itemDataSO = ItemDatabase.GetItemDataSO(item.itemID);
            Logging.Log($"Trying to add item: {itemDataSO.ItemName}");

            foreach (var slot in m_inventorySlots)
            {
                if (slot.InventoryItem == null)
                {
                    SpawnNewItem(itemDataSO, slot);
                    return;
                }
            }

            Logging.LogWarning("No empty inventory slots available!");
        }

        public void TryRemoveItem(ItemData item)
        {
            if (IsEmpty) return;

            var itemDataSO = ItemDatabase.GetItemDataSO(item.itemID);

            foreach (var slot in m_inventorySlots)
            {
                if (slot.InventoryItem.Item == itemDataSO)
                {
                    Destroy(slot.InventoryItem);
                    return;
                }
            }
        }

        public void SpawnNewItem(ItemDataSO item, InventorySlot slot)
        {
            InventoryItem newItem  = Instantiate(prefab);
            newItem.transform.SetParent(slot.transform);
            newItem.transform.localScale = Vector3.one;
            newItem.transform.localPosition = Vector3.zero;
            newItem.Initialize(this, item);
        }

        public void OnInventory()
        {
            m_acvite = !m_acvite;
            
            if (m_acvite)
            {
                Sequence sequence = DOTween.Sequence();
                sequence.AppendCallback(() => inventory.SetActive(m_acvite));
                sequence.Append(Rect.DOAnchorPosY(0, 0.2f).SetEase(Ease.OutCubic));
            }
            else
            {
                Sequence sequence = DOTween.Sequence();

                sequence.Append(Rect.DOAnchorPosY(150, 0.2f).SetEase(Ease.OutCubic));
                sequence.AppendCallback(() => inventory.SetActive(m_acvite));
            }
        }
    }
}

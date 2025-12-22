using System.Collections.Generic;
using System.Linq;
using _Scripts.Core.Events;
using _Scripts.Core.Input;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.YTH.Inventory
{    
    public class InventoryManager : MonoBehaviour
    {
        public bool IsEmpty => m_inventorySlots.Count == 0;
        public RectTransform Rect => m_rectTransform ??= inventory.GetComponent<RectTransform>();
        public InventorySlot SelectedSlot => m_selectedSlot;

        [Header("Inventory Settings")]
        [SerializeField] private InventoryItem prefab;
        [SerializeField] private InputSO inputSO;
        [SerializeField] private GameObject inventory;

        [Header("Event Channel")]
        [SerializeField] private ItemDataEventChannel itemAddEventChannel;
        [SerializeField] private ItemDataEventChannel itemRemoveEventChannel;
        [SerializeField] private ItemDataEventChannel itemCanAddEventChannel;
        [SerializeField] private ItemDataEventChannel itemCanRemoveEventChannel;
        [SerializeField] private InventoryManagerEventChannel inventoryManagerEventChannel;
        [SerializeField] private BoolEventChannel InventoryOpenEventChannel;
        [SerializeField] private BoolEventChannel InventoryUpdateEventChannel;
        [SerializeField] private AlertDataEventChannel alertDataEventChannel;

        private List<InventorySlot> m_inventorySlots;
        private bool m_acvite;
        private RectTransform m_rectTransform;     
        private InventorySlot m_selectedSlot = new();

        private void Awake()
        {
            m_inventorySlots = GetComponentsInChildren<InventorySlot>().ToList();

            itemCanAddEventChannel.OnEvent += CanAddItem;
            itemCanRemoveEventChannel.OnEvent += CanRemoveItem;
            itemAddEventChannel.OnEvent += TryAddItem;
            itemRemoveEventChannel.OnEvent += TryRemoveItem;
            inputSO.OnInventoryed += OnInventory;
            inputSO.OnNumbersPressed += SelecteSlot;
            inputSO.OnUsed += UseSelectedItem;
        }

        private void Start()
        {
            foreach (var slot in m_inventorySlots)
            {
                slot.Initialize(this);
            }
            
            inventoryManagerEventChannel.Raise(this);
            SelecteSlot(1);
        }

        private void OnDestroy()
        {
            itemCanAddEventChannel.OnEvent -= CanAddItem;
            itemCanRemoveEventChannel.OnEvent -= CanRemoveItem;
            itemAddEventChannel.OnEvent -= TryAddItem;
            itemRemoveEventChannel.OnEvent -= TryRemoveItem;
            inputSO.OnInventoryed -= OnInventory;
            inputSO.OnNumbersPressed -= SelecteSlot;
            inputSO.OnUsed -= UseSelectedItem;
        }

        public void SelecteSlot(int index)
        {
            int count = index - 1;
            var inventorySlot = m_inventorySlots[count];

            if (inventorySlot != null)
            {
                foreach (var slot in m_inventorySlots)
                {
                    slot.UnSelect();
                }

                inventorySlot.Select();
                m_selectedSlot = inventorySlot;
            }
            
            if (inventorySlot.InventoryItem != null)
            {
                alertDataEventChannel.Raise(new($"- {inventorySlot.InventoryItem.Item.ItemName} -", $"{inventorySlot.InventoryItem.Item.Description}", 1.5f, 0.2f));
            }
        }

        public void UseSelectedItem()
        {
            if (m_selectedSlot.InventoryItem != null)
            {
                if (m_selectedSlot.InventoryItem.Item.IsConsumable)
                {
                    foreach (var effect in m_selectedSlot.InventoryItem.Item.UseEffects)
                    {
                        effect.ApplyEffect(this.gameObject);
                        Destroy(m_selectedSlot.InventoryItem.gameObject);
                        alertDataEventChannel.Raise(new($"- 아이템을 사용했습니다. -", $"{m_selectedSlot.InventoryItem.Item.ItemName}", 1.5f, 0.2f));
                    }
                }
            }
        }

        public void CanAddItem(ItemData item)
        {
            foreach (var slot in m_inventorySlots)
            {
                if (slot.InventoryItem == null)
                {
                    InventoryUpdateEventChannel.Raise(true);
                    return;
                }
            }

            InventoryUpdateEventChannel.Raise(false);
            return;   
        }

        public void CanRemoveItem(ItemData item)
        {
            if (IsEmpty)
            {
                InventoryUpdateEventChannel.Raise(false);
                return;
            }

            var itemDataSO = ItemDatabase.GetItemDataSO(item.itemID);

            foreach (var slot in m_inventorySlots)
            {
                if (slot.InventoryItem == null)
                {
                    continue;
                }
                if (slot.InventoryItem.Item == itemDataSO)
                {
                    InventoryUpdateEventChannel.Raise(true);
                    return;
                }
            }

            InventoryUpdateEventChannel.Raise(false);
            return;
        }

        public void TryAddItem(ItemData item)
        {
            var itemDataSO = ItemDatabase.GetItemDataSO(item.itemID);

            foreach (var slot in m_inventorySlots)
            {
                if (slot.InventoryItem == null)
                {
                    SpawnNewItem(itemDataSO, slot);
                    return;
                }
            }

        }

        public void TryRemoveItem(ItemData item)
        {
            if (IsEmpty) return;

            var itemDataSO = ItemDatabase.GetItemDataSO(item.itemID);

            foreach (var slot in m_inventorySlots)
            {
                if (slot.InventoryItem == null)
                {
                    continue;
                }
                if (slot.InventoryItem.Item == itemDataSO)
                {
                    Destroy(slot.InventoryItem.gameObject);
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
                if (Rect != null)
                {    
                    Sequence sequence = DOTween.Sequence();
                    Rect.DOKill();
                    sequence.AppendCallback(() => inventory.SetActive(m_acvite));
                    sequence.Append(Rect.DOAnchorPosY(0, 0.2f).SetEase(Ease.OutCubic));
                }
            }
            else
            {
                if (Rect != null)
                { 
                    Sequence sequence = DOTween.Sequence();
                    Rect.DOKill();
                    sequence.Append(Rect.DOAnchorPosY(150, 0.2f).SetEase(Ease.OutCubic));
                    sequence.AppendCallback(() => inventory.SetActive(m_acvite));
                }
            }
        }
    }
}

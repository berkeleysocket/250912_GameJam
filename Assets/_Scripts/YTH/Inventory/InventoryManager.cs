using System.Collections.Generic;
using System.Linq;
using _Scripts.Core.Events;
using _Scripts.Core.Utility;
using UnityEngine;

namespace _Scripts.Core.YTH.Inventory
{    
    public class InventoryManager : MonoBehaviour
    {

        public bool IsEmpty => inventorySlots.Count == 0;

        [SerializeField] private InventoryItem prefab;
        [SerializeField] private ItemDataEventChannel itemAddEventChannel;
        [SerializeField] private ItemDataEventChannel itemRemoveEventChannel;
        [SerializeField] private InventoryManagerEventChannel inventoryManagerEventChannel;
        [SerializeField] private BoolEventChannel boolEventChannel;

        [SerializeField] private List<InventorySlot> inventorySlots;
    

        private void Awake()
        {
            inventorySlots = GetComponentsInChildren<InventorySlot>().ToList();

            itemAddEventChannel.OnEvent += TryAddItem;
        }

        private void Start()
        {
            foreach (var slot in inventorySlots)
            {
                slot.Initialize(this);
            }
            
            inventoryManagerEventChannel.Raise(this);
        }

        private void OnDestroy()
        {
            itemAddEventChannel.OnEvent -= TryAddItem;
        }

        public void TryAddItem(ItemData item)
        {
            foreach (var slot in inventorySlots)
            {
                if (slot.InventoryItem != null)
                {
                    SpawnNewItem(ItemDatabase.GetItemDataSO(item.itemID), slot);
                    return;
                }
            }
        }

        public void TryRemoveItem(ItemData item)
        {
            if (IsEmpty) return;

            var itemDataSO = ItemDatabase.GetItemDataSO(item.itemID);

            foreach (var slot in inventorySlots)
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
    }
}

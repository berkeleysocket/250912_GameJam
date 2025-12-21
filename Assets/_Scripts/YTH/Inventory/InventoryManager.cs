using System.Collections.Generic;
using _Scripts.Core.Events;
using UnityEngine;

namespace _Scripts.Core.YTH.Inventory
{    
    public class InventoryManager : MonoBehaviour
    {

        [field:SerializeField] public List<InventorySlot> inventorySlots { get; private set; }
        public bool IsEmpty => inventorySlots.Count == 0;

        [SerializeField] private InventoryItem prefab;
        [SerializeField] private ItemDataEventChannel itemAddEventChannel;
        [SerializeField] private InventoryManagerEventChannel inventoryManagerEventChannel;

    

        private void Awake()
        {
            itemAddEventChannel.OnEvent += AddItem;
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
            itemAddEventChannel.OnEvent -= AddItem;
        }

        public void AddItem(ItemData item)
        {
            if (IsEmpty) return;

            foreach (var slot in inventorySlots)
            {
                if (slot.InventoryItem != null)
                {
                    SpawnNewItem(ItemDatabase.GetItemDataSO(item.itemID), slot);
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

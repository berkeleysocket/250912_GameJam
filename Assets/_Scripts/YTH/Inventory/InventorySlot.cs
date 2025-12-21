using UnityEngine;

namespace _Scripts.Core.YTH.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        [field:SerializeField] public InventoryItem InventoryItem { get; private set; }

        protected InventoryManager m_inventoryManager;

        protected virtual void OnTransformChildrenChanged()
        {
            InventoryItem = GetComponentInChildren<InventoryItem>();
        }

        public virtual void Initialize(InventoryManager inventoryManager)
        {
            this.m_inventoryManager = inventoryManager;
        }

    }
}

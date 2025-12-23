using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.YTH.Inventory
{
    public class InventorySlot : MonoBehaviour
    {
        [field:SerializeField] public InventoryItem InventoryItem { get; private set; }
        public bool IsSelected => m_isSelected;
        [Header("Slot Settings")]
        [SerializeField] private Color selectedColor;
        [SerializeField] private Color unSelectedColor;
        [SerializeField] private Image slotImage;

        protected InventoryManager m_inventoryManager;
        private bool m_isSelected;

        protected virtual void OnTransformChildrenChanged()
        {
            InventoryItem = GetComponentInChildren<InventoryItem>();
        }

        public void Reset()
        {
            if (InventoryItem != null)
            {
                Destroy(InventoryItem.gameObject);
            }
            InventoryItem = null;
        }

        public virtual void Initialize(InventoryManager inventoryManager)
        {
            this.m_inventoryManager = inventoryManager;
        }

        public void Select()
        {
            slotImage.color = selectedColor;
            m_isSelected = true;
        }

        public void UnSelect()
        {
            slotImage.color = unSelectedColor;
            m_isSelected = false;
        }
    }
}

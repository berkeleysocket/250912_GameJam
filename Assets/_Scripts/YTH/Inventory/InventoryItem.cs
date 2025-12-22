using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.YTH.Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        [field:SerializeField] public ItemDataSO Item { get; private set; }
        public RectTransform RectTransform => m_rectTransform ??= transform as RectTransform;
        
        [HideInInspector] public Transform parentAfterDrag;
        
        [SerializeField] private Image itemIcon;

        private InventoryManager m_inventoryManager;
        private RectTransform m_rectTransform;

        public void Initialize(InventoryManager inventoryManager, ItemDataSO itemDataSO)
        {
            this.m_inventoryManager = inventoryManager;
            SetItemData(itemDataSO); 
            itemIcon.raycastTarget = true;   
            transform.localPosition = Vector2.zero; 
        }


        public void SetItemData(ItemDataSO itemDataSO)
        {
            this.Item = itemDataSO;
            UpdateUI();
        }


        private void UpdateUI()
        {
            if (Item != null) itemIcon.sprite = Item.Icon;
        }


    }
}
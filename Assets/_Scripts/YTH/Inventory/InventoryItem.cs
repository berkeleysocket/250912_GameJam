using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.Core.YTH.Inventory
{
    public class InventoryItem : MonoBehaviour
    {
        [field:SerializeField] public ItemDataSO Item { get; private set; }
        [field:SerializeField] public int Count { get; private set; }
        public RectTransform RectTransform => m_rectTransform ??= transform as RectTransform;
        
        [HideInInspector] public Transform parentAfterDrag;
        
        [SerializeField] private Image itemIcon;

        private InventoryManager m_inventoryManager;
        private RectTransform m_rectTransform;

        public void Initialize(InventoryManager inventoryManager, ItemDataSO itemDataSO)
        {
            this.m_inventoryManager = inventoryManager;
            SetItemData(itemDataSO); 
            Count = 1;
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
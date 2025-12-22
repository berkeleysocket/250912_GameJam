using _Scripts.Core.Events;
using _Scripts.YTH.Inventory;
using UnityEngine;

namespace _Scripts.YTH.Temp
{    
    public class TestAdd : MonoBehaviour
    {
        [SerializeField] private ItemDataEventChannel canAddItemEventChannel;
        [SerializeField] private ItemDataEventChannel addItemEventChannel;
        [SerializeField] private BoolEventChannel inventoryUpdateEventChannel;
        [SerializeField] private ItemData itemData;

        public void Add()
        {
            inventoryUpdateEventChannel.OnEvent += OnInventoryUpdate;
            canAddItemEventChannel.Raise(itemData);
        }

        private void OnInventoryUpdate(bool active)
        {
            if (active)
            {
                addItemEventChannel.Raise(itemData);
            }
            inventoryUpdateEventChannel.OnEvent -= OnInventoryUpdate;
        }
    }
}

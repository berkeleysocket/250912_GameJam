using _Scripts.Core.Events;
using _Scripts.Core.Utility;
using _Scripts.YTH.Inventory;
using UnityEngine;

namespace _Scripts.YTH.Temp
{    
    public class TestRemove : MonoBehaviour
    {
        [SerializeField] private ItemDataEventChannel canRemoveItemEventChannel;
        [SerializeField] private ItemDataEventChannel removeItemEventChannel;
        [SerializeField] private BoolEventChannel inventoryUpdateEventChannel;
        [SerializeField] private ItemData itemData;

        public void Remove()
        {
            inventoryUpdateEventChannel.OnEvent += OnInventoryUpdate;
            canRemoveItemEventChannel.Raise(itemData);
        }

        private void OnInventoryUpdate(bool active)
        {
            if (active)
            {
                removeItemEventChannel.Raise(itemData);
            }
            inventoryUpdateEventChannel.OnEvent -= OnInventoryUpdate;
        }
    }
}

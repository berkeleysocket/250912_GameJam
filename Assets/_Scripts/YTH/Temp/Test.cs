using _Scripts.Core.Events;
using _Scripts.Core.YTH.Inventory;
using UnityEngine;

public class Test : MonoBehaviour
{
    [SerializeField] private ItemDataEventChannel itemDataEventChannel;
    [SerializeField] private ItemData itemData;

    public void Add()
    {
        itemDataEventChannel.Raise(itemData);
    }
}

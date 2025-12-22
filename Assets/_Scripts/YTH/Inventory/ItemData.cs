using System;

namespace _Scripts.Core.YTH.Inventory
{
    [Serializable]
    public class ItemData
    {
        public int itemID;

        public ItemData(int itemID)
        {
            this.itemID = itemID;
        }
    }
}

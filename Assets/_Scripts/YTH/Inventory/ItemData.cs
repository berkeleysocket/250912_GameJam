using System;

namespace _Scripts.Core.YTH.Inventory
{
    [Serializable]
    public class ItemData
    {
        public int itemID;
        public int count;

        public ItemData(int itemID)
        {
            this.itemID = itemID;
        }
    }
}

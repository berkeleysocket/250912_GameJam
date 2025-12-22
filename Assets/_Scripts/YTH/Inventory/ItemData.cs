using System;

namespace _Scripts.YTH.Inventory
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

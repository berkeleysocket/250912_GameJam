using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.Core.YTH.Inventory
{
    [CreateAssetMenu(fileName = "ItemDataSO", menuName = "SO/Item/ItemData")]
    public class ItemDataSO : ScriptableObject
    {
        [field:Header("Item Settings")]
        [field:SerializeField] public string ItemName { get; private set; }
        [field:SerializeField, TextArea] public string Description { get; private set; }
        [field:SerializeField] public Sprite Icon { get; private set; }
        [field:SerializeField] public int ItemID { get; private set; }

        public override string ToString() => ItemName;
        public override int GetHashCode() => ItemID;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(this, obj)) return true;
            if (obj is not ItemDataSO other) return false;

            return ItemID == other.ItemID;
        }

        public static bool operator ==(ItemDataSO lhs, ItemDataSO rhs)
        {
            if (ReferenceEquals(lhs, rhs)) return true;
            if (ReferenceEquals(lhs, null) || ReferenceEquals(rhs, null)) return false;

            return lhs.ItemID == rhs.ItemID;
        }

        public static bool operator !=(ItemDataSO lhs, ItemDataSO rhs)
        {
            return !(lhs == rhs);
        }
    }
}

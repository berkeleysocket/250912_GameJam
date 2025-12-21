using _Scripts.Core.Utility;
using UnityEngine;


namespace _Scripts.Core.YTH.Inventory
{
    public class ItemDatabase : MonoSingleton<ItemDatabase> {
        public ItemDatabaseSO ItemDatabaseSO => itemDatabaseSO ??= Resources.Load<ItemDatabaseSO>("ItemDatabaseSO");
        [SerializeField] private ItemDatabaseSO itemDatabaseSO;

        protected override void Awake() {
            base.Awake();
            ItemDatabaseSO.Initialize();
            DontDestroyOnLoad(this);
        }

        public ItemDataSO GetItemData(int itemID) => ItemDatabaseSO[itemID];

        public static ItemDataSO GetItemDataSO(int itemID) {
            return Instance.GetItemData(itemID);
        }

        public ItemDataSO this[int itemID] => ItemDatabaseSO[itemID];
    }
}
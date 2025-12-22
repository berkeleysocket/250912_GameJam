using System.Collections.Generic;
using System.Linq;
using _Scripts.Core.Utility;
#if UNITY_EDITOR
using UnityEditor;
#endif
using UnityEngine;

namespace _Scripts.YTH.Inventory
{
    [CreateAssetMenu(fileName = "ItemDatabaseSO", menuName = "SO/Item/Database")]
    public class ItemDatabaseSO : ScriptableObject {
        [field: SerializeField] public List<ItemDataSO> ItemDataList { get; private set; }

        private Dictionary<int, ItemDataSO> _itemDataDictionary;


        public void Initialize() {
            foreach (var item in ItemDataList) {
                if (item.ItemID <= 0) {
                    Logging.LogError($"{item.ItemName}({item.name})의 ItemID가 유효하지 않습니다: {item.ItemID}");
                }
            }

            _itemDataDictionary ??= ItemDataList.ToDictionary(item => item.ItemID);
        }

        public ItemDataSO this[int hash] {
            get {
                if (_itemDataDictionary == null) {
                    Initialize();
                }

                return
                    _itemDataDictionary.GetValueOrDefault(hash);
            }
        }

#if UNITY_EDITOR
        private void CollectItems() {
            ItemDataList ??= new List<ItemDataSO>();
            ItemDataList.Clear();
            var guids = AssetDatabase.FindAssets("t:ItemDataSO");

            foreach (string guid in guids) {
                string path = AssetDatabase.GUIDToAssetPath(guid);
                ItemDataSO item = AssetDatabase.LoadAssetAtPath<ItemDataSO>(path);
                if (!item) continue;
                if (item.ItemID <= 0) {
                    Logging.LogError($"{item.ItemName}({item.name})의 ItemID가 유효하지 않습니다: {item.ItemID}");
                }

                if (ItemDataList.Contains(item)) {
                    Logging.LogError($"{item.ItemName}({item.name})와(과) 중복되는 ItemID가 존재합니다: {item.ItemID}");
                }
                ItemDataList.Add(item);
            }
        }

        [CustomEditor(typeof(ItemDatabaseSO))]
        public class ItemDatabaseSOEditor : Editor {
            public override void OnInspectorGUI() {
                base.OnInspectorGUI();
                if (GUILayout.Button("Collect Items")) {
                    ((ItemDatabaseSO)target).CollectItems();
                    EditorUtility.SetDirty(target);
                    AssetDatabase.SaveAssets();
                }
            }
        }
#endif
    }
}
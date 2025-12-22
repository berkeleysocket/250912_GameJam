using _Scripts.Core.Events;
using _Scripts.Core.Input;
using _Scripts.Core.Utility;
using _Scripts.YTH.Inventory;
using Ksy.Scripts.Player;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.YTH.Item
{    
    public class ItemObject : MonoBehaviour
    {
        [field: SerializeField] public ItemDataSO ItemData { get; private set; }

        [Header("Item Object Settings")]
        [SerializeField] private float pickupDistance = 2f;
        [SerializeField] private float pickupTime = 1f;
        [SerializeField] private InputSO inputSO;
        [SerializeField] private GameObject keyPrompt;
        [Header("Event Channels")]
        [SerializeField] private ItemDataEventChannel canAddItemEventChannel;
        [SerializeField] private ItemDataEventChannel addItemEventChannel;
        [SerializeField] private BoolEventChannel inventoryUpdateEventChannel;

        private Player m_player;

        private float m_pickupTime;
        private bool m_canPickup;
        private bool m_requestSent;
        private bool m_promptShown;

        private float m_pickupDistanceSqr;

        private void Awake()
        {
            m_player = FindAnyObjectByType<Player>();
            m_pickupDistanceSqr = pickupDistance * pickupDistance;

            inputSO.OnInteracted += TryPickUp;

            // 초기 UI
            if (keyPrompt) keyPrompt.SetActive(false);
        }

        private void Update()
        {
            if (!m_player) return;

            // 거리 체크 (sqrt 제거)
            Vector3 delta = m_player.transform.position - transform.position;
            m_canPickup = delta.sqrMagnitude <= m_pickupDistanceSqr;

            // 프롬프트는 상태 변할 때만
            if (keyPrompt && m_promptShown != m_canPickup)
            {
                m_promptShown = m_canPickup;
                keyPrompt.SetActive(m_canPickup);
            }
            if (!m_requestSent && m_pickupTime >= pickupTime)
            {
                m_requestSent = true;
                inventoryUpdateEventChannel.OnEvent += OnInventoryUpdate;
                canAddItemEventChannel.Raise(new ItemData(ItemData.ItemID));
            }
        }

        private void OnDestroy()
        {
            inputSO.OnInteracted -= TryPickUp;
            inventoryUpdateEventChannel.OnEvent -= OnInventoryUpdate; // 안전하게 제거
        }

        private void TryPickUp(bool interacted)
        {
            m_requestSent = true;
            inventoryUpdateEventChannel.OnEvent += OnInventoryUpdate;
            canAddItemEventChannel.Raise(new ItemData(ItemData.ItemID));
        }

        private void OnInventoryUpdate(bool active)
        {
            inventoryUpdateEventChannel.OnEvent -= OnInventoryUpdate;

            if (!active) // 인벤토리 꽉 찼다 등
            {
                // 다음 시도 가능하게 풀어주고, 게이지도 취향껏 유지/리셋
                m_requestSent = false;
                return;
            }

            addItemEventChannel.Raise(new ItemData(ItemData.ItemID));
            Destroy(gameObject);
        }
    }

}

using _Scripts.Core.Events;
using _Scripts.Core.Input;
using _Scripts.YTH.Inventory;
using Ksy.Scripts.Player;
using UnityEngine;

namespace _Scripts.YTH.Item
{    
    public class ItemObject : MonoBehaviour
    {
        [field:SerializeField] public ItemDataSO ItemData { get; private set; }
        [SerializeField] private ItemDataEventChannel canAddItemEventChannel;
        [SerializeField] private ItemDataEventChannel addItemEventChannel;
        [SerializeField] private BoolEventChannel inventoryUpdateEventChannel;
        [SerializeField] private float pickupDistance = 2f;
        [SerializeField] private float pickupTime;
        [SerializeField] private InputSO inputSO;
        
        private float m_pickupTime;
        private bool m_canPickup;
        private float m_distance;
        private Player m_player;


        private void Awake()
        {
            m_player = FindAnyObjectByType<Player>();

            inputSO.OnInteracted += TryPickUp;
        }

        private void Update()
        {
            m_distance = Vector3.Distance(transform.position, m_player.transform.position);
            m_canPickup = m_distance <= pickupDistance;

            if (!m_canPickup)
            {
                m_pickupTime -= Time.deltaTime;
                m_pickupTime = Mathf.Clamp(m_pickupTime, 0f, pickupTime);
            }
            
        }

        private void OnDestroy()
        {
            inputSO.OnInteracted -= TryPickUp;
        }

        private void TryPickUp(bool interacted)
        {
            if (interacted && m_canPickup)
            {    
                m_pickupTime += Time.deltaTime;
                if (m_pickupTime >= pickupTime)
                {
                    inventoryUpdateEventChannel.OnEvent += OnInventoryUpdate;
                    canAddItemEventChannel.Raise(new ItemData(ItemData.ItemID));
                }
            }
        }


        private void OnInventoryUpdate(bool active)
        {
            if (active)
            {
                addItemEventChannel.Raise(new ItemData(ItemData.ItemID));
                Destroy(gameObject);
            }
            inventoryUpdateEventChannel.OnEvent -= OnInventoryUpdate;
        }
    }
}

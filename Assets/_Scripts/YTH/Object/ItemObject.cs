using _Scripts.Core.Events;
using _Scripts.Core.Input;
using _Scripts.Core.Utility;
using _Scripts.YTH.Inventory;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.YTH.Object
{    
    public class ItemObject : MonoBehaviour
    {
        [field: SerializeField] public ItemDataSO ItemData { get; private set; }

        [Header("Item Object Settings")]
        [SerializeField] private float pickupDistance;
        [SerializeField] private InputSO inputSO;
        [SerializeField] private GameObject keyPrompt;
        [SerializeField] private SpriteRenderer spriteRenderer;
        [SerializeField] private CircleCollider2D itemCollider;

        [Header("Event Channels")]
        [SerializeField] private ItemDataEventChannel canAddItemEventChannel;
        [SerializeField] private ItemDataEventChannel addItemEventChannel;
        [SerializeField] private BoolEventChannel inventoryUpdateEventChannel;


        private void Awake()
        {
            spriteRenderer.sprite = ItemData.Icon;
            keyPrompt.SetActive(false);
            itemCollider.radius = pickupDistance;
        }

        public void SetItemData(ItemDataSO itemData)
        {
            ItemData = itemData;
            spriteRenderer.sprite = ItemData.Icon;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (keyPrompt != null)
                {
                    Sequence seq = DOTween.Sequence();
                    keyPrompt.transform.DOKill();
                    keyPrompt.transform.localScale = Vector3.zero;
                    seq.AppendCallback(() => keyPrompt.SetActive(true));
                    seq.Append(keyPrompt.transform.DOScale(1f, 0.2f).SetEase(Ease.OutCubic));
                }
                Logging.Log("Can Pick Up");
                inputSO.OnInteracted += Add;
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                if (keyPrompt != null)
                {
                    Sequence seq = DOTween.Sequence();
                    keyPrompt.transform.DOKill();
                    seq.Append(keyPrompt.transform.DOScale(0f, 0.2f).SetEase(Ease.OutCubic));
                    seq.AppendCallback(() => keyPrompt.SetActive(false));
                }
                Logging.Log("Can't Pick Up");
                inputSO.OnInteracted -= Add;
            }
        }


        public void Add()
        {
            Logging.Log("Can Add");
            inventoryUpdateEventChannel.OnEvent += OnInventoryUpdate;
            canAddItemEventChannel.Raise(new(ItemData.ItemID));
        }

        private void OnInventoryUpdate(bool active)
        {
            if (active)
            {
                addItemEventChannel.Raise(new(ItemData.ItemID));
                Destroy(gameObject);
                Logging.Log("Add");
            }
            inventoryUpdateEventChannel.OnEvent -= OnInventoryUpdate;
        }

    }

}

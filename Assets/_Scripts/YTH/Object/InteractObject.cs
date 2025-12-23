using _Scripts.Core.Events;
using _Scripts.Core.Input;
using _Scripts.Core.Utility;
using _Scripts.YTH.Inventory;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.YTH.Object
{    
    public class InteractObject : MonoBehaviour
    {

        [Header("Item Object Settings")]
        [SerializeField] private float pickupDistance;
        [SerializeField] private InputSO inputSO;
        [SerializeField] private GameObject keyPrompt;
        [SerializeField] private CircleCollider2D itemCollider;

        [Header("Event Channels")]
        [SerializeField] private EmptyEventChannel InteractedEventChannel;


        private void Awake()
        {
            keyPrompt.SetActive(false);
            itemCollider.radius = pickupDistance;
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
                inputSO.OnInteracted += Interact;
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
                inputSO.OnInteracted -= Interact;
            }
        }


        public void Interact()
        {
            InteractedEventChannel.Raise(new());
        }

    }

}

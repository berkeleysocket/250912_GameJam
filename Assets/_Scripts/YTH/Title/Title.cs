using _Scripts.Core.Events;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.YTH.Title
{    
    public class Title : MonoBehaviour
    {
        [Header("Title Settings")]
        [SerializeField] private RectTransform parentTransform;
        [SerializeField] private Image backgroundPanel;
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI subTitleText;
        [SerializeField] private AudioSource audioSource;

        [Header("Event Channels")]
        [SerializeField] private TitleDataEventChannel titleDataEventChannel;


        private void Awake()
        {
            parentTransform.gameObject.SetActive(false);
            titleDataEventChannel.OnEvent += ShowTitle;
        }

        private void OnDestroy()
        {
             titleDataEventChannel.OnEvent -= ShowTitle;
        }

        private void ShowTitle(TitleData titleData)
        {
            if (parentTransform != null)
            {
                Sequence sequence = DOTween.Sequence().SetUpdate(true);
                parentTransform.DOKill();
                sequence.AppendCallback(() =>
                {
                    parentTransform.gameObject.SetActive(true);
                    titleText.text = titleData.titleText;
                    subTitleText.text = titleData.subTitleText;
                    iconImage.sprite = titleData.iconImage;
                    if (audioSource != null && titleData.audioClip != null)
                    {
                        audioSource.PlayOneShot(titleData.audioClip);
                    }
                }).SetUpdate(true);
                sequence.Append(parentTransform.DOScaleY(1f, titleData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                if (iconImage != null && titleData.iconImage != null)
                {
                    sequence.Join(iconImage.DOFade(1f, titleData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                }
                sequence.Join(titleText.DOFade(1f, titleData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                sequence.Join(subTitleText.DOFade(1f, titleData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                sequence.AppendInterval(titleData.duration);
                sequence.Append(parentTransform.DOScaleY(0f, titleData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                if (iconImage != null && titleData.iconImage != null)
                {
                    sequence.Join(iconImage.DOFade(0f, titleData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                }
                sequence.Join(titleText.DOFade(0f, titleData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                sequence.Join(subTitleText.DOFade(0f, titleData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
            }
        }
    }
}

using _Scripts.Core.Events;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace _Scripts.YTH.Alert
{
    public class Alert : MonoBehaviour
    {
        [Header("Alert Settings")]
        [SerializeField] private RectTransform parentTransform;
        [SerializeField] private TextMeshProUGUI titleText;
        [SerializeField] private TextMeshProUGUI subTitleText;

        [Header("Event Channels")]
        [SerializeField] private AlertDataEventChannel alertDataEventChannel;

        private void Awake()
        {
            parentTransform.anchoredPosition = new Vector2(0, -120);
            alertDataEventChannel.OnEvent += ShowAlert;
        }

        private void OnDestroy()
        {
             alertDataEventChannel.OnEvent -= ShowAlert;
        }

        private void ShowAlert(AlertData alertData)
        {
            if (parentTransform != null)
            {
                Sequence sequence = DOTween.Sequence().SetUpdate(true);
                parentTransform.DOKill();
                sequence.AppendCallback(() =>
                {
                    titleText.text = alertData.title;
                    subTitleText.text = alertData.subTitle;
                }).SetUpdate(true);
                sequence.Append(parentTransform.DOAnchorPosY(80, alertData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                sequence.Join(titleText.DOFade(1f, alertData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                sequence.Join(subTitleText.DOFade(1f, alertData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                sequence.AppendInterval(alertData.duration).SetUpdate(true);
                sequence.Append(parentTransform.DOAnchorPosY(-120, alertData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                sequence.Join(titleText.DOFade(0f, alertData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
                sequence.Join(subTitleText.DOFade(0f, alertData.animationDuration).SetEase(Ease.OutBack)).SetUpdate(true);
            }
        }


    }
}

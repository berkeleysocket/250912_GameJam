using _Scripts.Core.Events;
using _Scripts.Core.Input;
using _Scripts.Core.Structs;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.YTH.Setting
{    
    public class Setting : MonoBehaviour
    {
        [Header("Setting Settings")]
        [SerializeField] private RectTransform setting;
        [SerializeField] private InputSO inputSO;

        [Header("Event Channel")]
        [SerializeField] private EmptyEventChannel toggleSettingEventChannel;

        private bool m_isSettingActive = false;

        private void Awake()
        {
            setting.anchoredPosition = new Vector3(0, 1200, 0);
            setting.gameObject.SetActive(m_isSettingActive);

            inputSO.OnSettingPressed += ToggleSetting;
        }
        

        private void OnDestroy()
        {
            inputSO.OnSettingPressed -= ToggleSetting;
        }

        public void ToggleSetting()
        {
            m_isSettingActive = !m_isSettingActive;
            
            if (m_isSettingActive)
            {
                if (setting != null)
                {    
                    Sequence sequence = DOTween.Sequence();
                    setting.DOKill();
                    sequence.AppendCallback(() => setting.gameObject.SetActive(m_isSettingActive)).SetUpdate(true);
                    sequence.Append(setting.DOAnchorPosY(0, 0.25f).SetEase(Ease.OutCubic)).SetUpdate(true);
                    sequence.AppendCallback(() => Time.timeScale = 0);
                }
            }
            else
            {
                if (setting != null)
                { 
                    Sequence sequence = DOTween.Sequence();
                    setting.DOKill();
                    sequence.Append(setting.DOAnchorPosY(1200, 0.25f).SetEase(Ease.OutCubic)).SetUpdate(true);
                    sequence.AppendCallback(() => setting.gameObject.SetActive(m_isSettingActive)).SetUpdate(true);
                    sequence.AppendCallback(() => Time.timeScale = 1);
                }
            }
        }
    }
}

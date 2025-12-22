using System.Collections.Generic;
using _Scripts.Core.Input;
using _Scripts.YTH.Translation;
using DG.Tweening;
using UnityEngine;

namespace _Scripts.YTH.Guide
{    
    public class GuideManager : MonoBehaviour
    {
        [Header("Guide Manager Settings")]
        [SerializeField] private RectTransform guide;
        [SerializeField] private InputSO inputSO;
        [SerializeField] private List<TranslationDataSO> guideDataList;
        
        private GuideSlot[] m_guideSlots;
        private bool m_isGuideActive = false;
        private void Awake()
        {
            m_guideSlots = guide.GetComponentsInChildren<GuideSlot>();
            guide.gameObject.SetActive(m_isGuideActive);
            guide.anchoredPosition = new Vector2(850, 0);

            for (int i = 0; i < m_guideSlots.Length; i++)
            {
                if (i < guideDataList.Count)
                {
                    m_guideSlots[i].SetGuideSlot(guideDataList[i]);
                }
                else
                {
                    m_guideSlots[i].gameObject.SetActive(false);
                }
            }

            inputSO.OnGuidePressed += ToggleGuide;
        }

        private void OnDestroy()
        {
            inputSO.OnGuidePressed -= ToggleGuide;
        }

        public void ToggleGuide()
        {
            m_isGuideActive = !m_isGuideActive;
            
            if (m_isGuideActive)
            {
                if (guide != null)
                {    
                    Sequence sequence = DOTween.Sequence();
                    guide.DOKill();
                    sequence.AppendCallback(() => guide.gameObject.SetActive(m_isGuideActive));
                    sequence.Append(guide.DOAnchorPosX(-50, 0.25f).SetEase(Ease.OutCubic));
                }
            }
            else
            {
                if (guide != null)
                { 
                    Sequence sequence = DOTween.Sequence();
                    guide.DOKill();
                    sequence.Append(guide.DOAnchorPosX(850, 0.25f).SetEase(Ease.OutCubic));
                    sequence.AppendCallback(() => guide.gameObject.SetActive(m_isGuideActive));
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Scripts.Core.Events;
using _Scripts.Core.Input;
using _Scripts.Core.Structs;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.YTH.Translation
{
    public class TranslationManager : MonoBehaviour
    {
        [Header("Translation Manager Settings")]
        [SerializeField] private InputSO inputSO;
        [SerializeField] private RectTransform panel;
        [SerializeField] private Transform content;
        [SerializeField] private Image iconPrefab;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private int level = 5;

        [Header("Event Channel")]
        [SerializeField] private EmptyEventChannel toggleTranslationEventChannel;
        [SerializeField] private EmptyEventChannel toggleGuideEventChannel;

        private List<TranslationDataSO> translationDatas = new();
        private bool m_isTranslationActive = false;

        private void Awake()
        {
            panel.gameObject.SetActive(m_isTranslationActive);
            panel.anchoredPosition = new Vector2(0, 1000);
            
            toggleTranslationEventChannel.OnEvent += ToggleTranslation;
        }

        private void OnDestroy()
        {
            toggleTranslationEventChannel.OnEvent -= ToggleTranslation;
        }
        

        [ContextMenu("Test")]
        public void Test()
        {
            for (int i = 0; i < level; i++)
            {
                var translationData = TranslationText.Instance.GetRandomTranslationData();
                if (translationData != null)
                {
                    translationDatas.Add(translationData);
                    var icon = Instantiate(iconPrefab, content);
                    icon.sprite = translationData.sprite;
                }
            }
        }

        public void Check()
        {
            string userInput = inputField.text;
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < translationDatas.Count; i++)
            {
                result.Append(TranslationText.Instance.GetTranslationToEnglish(translationDatas[i].Key));
            }

            if (userInput.Equals(result.ToString(), StringComparison.OrdinalIgnoreCase))
            {
                Debug.Log("Correct!");
            }
            else
            {
                Debug.Log($"Incorrect! Correct answer is: {result}");
            }
        }

        public void ToggleGuide()
        {
            toggleGuideEventChannel.Raise(new Empty());
        }

        public void ToggleTranslation(Empty empty)
        {
            m_isTranslationActive = !m_isTranslationActive;
            
            if (m_isTranslationActive)
            {
                if (panel != null)
                {    
                    Sequence sequence = DOTween.Sequence();
                    panel.DOKill();
                    sequence.AppendCallback(() => panel.gameObject.SetActive(m_isTranslationActive));
                    sequence.Append(panel.DOAnchorPosY(0, 0.25f).SetEase(Ease.OutCubic));
                    sequence.AppendCallback(() => inputSO.Controls.Disable());
                }
            }
            else
            {
                if (panel != null)
                { 
                    Sequence sequence = DOTween.Sequence();
                    panel.DOKill();
                    sequence.Append(panel.DOAnchorPosY(1000, 0.25f).SetEase(Ease.OutCubic));
                    sequence.AppendCallback(() => panel.gameObject.SetActive(m_isTranslationActive));
                    sequence.AppendCallback(() => inputSO.Controls.Enable());
                }
            }
        }
        
    }
}

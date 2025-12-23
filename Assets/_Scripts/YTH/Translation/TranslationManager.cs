using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using _Scripts.Core.Events;
using _Scripts.Core.Input;
using _Scripts.Core.Structs;
using _Scripts.YTH.Title;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.YTH.Translation
{
    public class TranslationManager : MonoBehaviour
    {
        [Header("Translation Manager Settings")]
        [SerializeField] private Sprite checkIcon;
        [SerializeField] private Sprite failIcon;
        [SerializeField] private InputSO inputSO;
        [SerializeField] private RectTransform panel;
        [SerializeField] private Transform content;
        [SerializeField] private Image iconPrefab;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private int level = 3;
        [SerializeField] private AudioClip checkSound;
        [SerializeField] private AudioClip xSound;
        [SerializeField] private Image line;

        [Header("Event Channel")]
        [SerializeField] private IntEventChannel levelEventChannel;
        [SerializeField] private EmptyEventChannel toggleTranslationEventChannel;
        [SerializeField] private EmptyEventChannel toggleGuideEventChannel;
        [SerializeField] private EmptyEventChannel levelDownEventChannel;
        [SerializeField] private EmptyEventChannel levelUpEventChannel;
        [SerializeField] private TitleDataEventChannel titleDataEventChannel;
        [SerializeField] private EmptyEventChannel doneTranslationEventChannel;
        [SerializeField] private IntEventChannel currentTranslationEventChannel;
        [SerializeField] private EmptyEventChannel directorSpeedUpEventChannel;

        private List<TranslationDataSO> translationDatas = new();
        private bool m_isTranslationActive = false;
        private int m_currentTranslation = 0;
        private bool m_working;
        private float m_endTime;
        private float m_time = 0;

        private void Awake()
        {
            panel.gameObject.SetActive(m_isTranslationActive);
            panel.anchoredPosition = new Vector2(0, 1000);

            line.fillAmount = 0;
            
            toggleTranslationEventChannel.OnEvent += ToggleTranslation;
            levelEventChannel.OnEvent += SetLevel;
            levelDownEventChannel.OnEvent += DownLevel;
            levelUpEventChannel.OnEvent += UpLevel;
        }

        private void Update()
        {
            if (m_working)
            {   
                m_time += Time.unscaledDeltaTime;
                
                line.fillAmount = 1 - ( m_time / m_endTime );
                if (m_time >= m_endTime)
                {
                    titleDataEventChannel.Raise(new TitleData(
                        failIcon,
                        "- 실패했습니다. -", 
                        $"업무 난이도가 상승합니다. 남은 업무 : {6-m_currentTranslation}개", 
                        xSound,
                        2.5f,
                        0.5f
                    ));
                    UpLevel(new());
                    UpLevel(new());
                    ToggleTranslation(new Empty());
                    doneTranslationEventChannel.Raise(new Empty());
                    directorSpeedUpEventChannel.Raise(new Empty());
                    m_working = false;
                }
            }
        }

        private void OnDestroy()
        {
            toggleTranslationEventChannel.OnEvent -= ToggleTranslation;
            levelEventChannel.OnEvent -= SetLevel;
            levelDownEventChannel.OnEvent -= DownLevel;
            levelUpEventChannel.OnEvent -= UpLevel;
        }
        
        public void Work()
        {
            m_endTime = (5 * level) + 20;
            m_time = 0;
            m_working = true;

            translationDatas.Clear();
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
            inputField.text = string.Empty;
            
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
                titleDataEventChannel.Raise(new TitleData(
                    checkIcon,
                    "- 성공했습니다. -", 
                    $"남은 업무 : {6-m_currentTranslation}개", 
                    checkSound,
                    2.5f,
                    0.5f
                ));
                UpLevel(new());
                ToggleTranslation(new Empty());
                doneTranslationEventChannel.Raise(new Empty());
            }
            else
            {
                titleDataEventChannel.Raise(new TitleData(
                    failIcon,
                    "- 실패했습니다. -", 
                    $"업무 난이도가 상승합니다. (정답: {result}), 남은 업무 : {6-m_currentTranslation}개", 
                    xSound,
                    2.5f,
                    0.5f
                ));
                UpLevel(new());
                UpLevel(new());
                ToggleTranslation(new Empty());
                doneTranslationEventChannel.Raise(new Empty());
                directorSpeedUpEventChannel.Raise(new Empty());
                

            }

            m_working = false;
            m_currentTranslation++;
            currentTranslationEventChannel.Raise(m_currentTranslation);

        }

        private void SetLevel(int level)
        {
            this.level = level;
        }

        private void DownLevel(Empty empty)
        {
            level = UnityEngine.Random.Range(1, level);
        }

        private void UpLevel(Empty empty)
        {
            level = UnityEngine.Random.Range(level, level + 3);
        }

        public void ToggleGuide()
        {
            toggleGuideEventChannel.Raise(new Empty());
        }

        public void ToggleTranslation(Empty empty)
        {
            m_isTranslationActive = !m_isTranslationActive;

            translationDatas.Clear();
            foreach (Transform child in content)
            {
                Destroy(child.gameObject);
            }
            inputField.text = string.Empty;
            
            if (m_isTranslationActive)
            {
                if (panel != null)
                {    
                    Sequence sequence = DOTween.Sequence().SetUpdate(true);
                    panel.DOKill();
                    sequence.AppendCallback(() => panel.gameObject.SetActive(m_isTranslationActive)).SetUpdate(true);
                    sequence.Append(panel.DOAnchorPosY(0, 0.25f).SetEase(Ease.OutCubic)).SetUpdate(true);
                    sequence.AppendCallback(() => inputSO.Controls.Disable()).SetUpdate(true);
                    sequence.AppendCallback(() => Work()).SetUpdate(true);
                    sequence.AppendCallback(() => Time.timeScale = 0);
                }
            }
            else
            {
                if (panel != null)
                { 
                    Sequence sequence = DOTween.Sequence().SetUpdate(true);
                    panel.DOKill();
                    sequence.Append(panel.DOAnchorPosY(1000, 0.25f).SetEase(Ease.OutCubic)).SetUpdate(true);
                    sequence.AppendCallback(() => panel.gameObject.SetActive(m_isTranslationActive)).SetUpdate(true);
                    sequence.AppendCallback(() => inputSO.Controls.Enable()).SetUpdate(true);
                    sequence.AppendCallback(() => Time.timeScale = 1);
                }
            }
        }
        
    }
}

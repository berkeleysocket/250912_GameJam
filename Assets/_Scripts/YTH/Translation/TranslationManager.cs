using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.YTH.Translation
{
    public class TranslationManager : MonoBehaviour
    {
        [Header("Translation Manager Settings")]
        [SerializeField] private RectTransform panel;
        [SerializeField] private Transform content;
        [SerializeField] private Image iconPrefab;
        [SerializeField] private TMP_InputField inputField;
        [SerializeField] private int level = 5;

        private List<TranslationDataSO> translationDatas = new();
        

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
        
    }
}

using _Scripts.YTH.Translation;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace _Scripts.YTH.Guide
{
    public class GuideSlot : MonoBehaviour
    {
        [SerializeField] private Image iconImage;
        [SerializeField] private TextMeshProUGUI resultText;

        public void SetGuideSlot(TranslationDataSO translationDataSO)
        {
            iconImage.sprite = translationDataSO.sprite;
            resultText.text = TranslationText.Instance.GetTranslationToEnglish(translationDataSO.Key);
        }
    }
}

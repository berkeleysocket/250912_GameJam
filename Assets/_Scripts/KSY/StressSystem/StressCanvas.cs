using _Scripts.Core.Events;
using DG.Tweening.Plugins;
using TMPro;
using UnityEngine;

namespace Ksy.Scripts.StressSystem
{
    public class StressCanvas : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI text;
        [SerializeField] private IntEventChannel stressUpdateEventChannel;

        private void Awake()
        {
            stressUpdateEventChannel.OnEvent -= UpdateUI;
        }

        private void OnDestroy()
        {
            stressUpdateEventChannel.OnEvent -= UpdateUI;
        }

        private void UpdateUI(int stress)
        {
            text.text = $"현재 스트레스 지수 : {stress}";
        }
    }
}

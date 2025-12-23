using _Scripts.Core.Events;
using _Scripts.Core.Structs;
using _Scripts.YTH.Title;
using UnityEngine;

namespace _Scripts.YTH.UseEffect
{
    [CreateAssetMenu(fileName = "SpeedUpUseEffectSO", menuName = "UseEffect/SpeedUp")]
    public class SpeedUpUseEffectSO : UseEffectBaseSO
    {
        [field:SerializeField] public float Duration { get; private set; }

        [Header("Speed Up Settings")]
        [SerializeField] private Sprite icon;
        [SerializeField] private AudioClip speedUpAudioClip;

        [Header("Event Channels")]
        [SerializeField] private TitleDataEventChannel titleDataEventChannel;
        [SerializeField] private SpeedDataEventChannel speedDataEventChannel;
        public override void ApplyEffect(GameObject target)
        {
            titleDataEventChannel.Raise(new TitleData
            {
                titleText = "- 파워에이드를 마셨습니다! -",
                subTitleText = $"힘이 넘쳐나 {Duration}초 동안 속도가 증가합니다.",
                iconImage = icon,
                duration = 2.5f,
                animationDuration = 0.5f,
                audioClip = speedUpAudioClip
            });
            speedDataEventChannel.Raise(new SpeedData(Efficacy, Duration));
        }
    }
}

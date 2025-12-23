using _Scripts.Core.Events;
using _Scripts.YTH.Title;
using _Scripts.YTH.UseEffect;
using UnityEngine;

[CreateAssetMenu(fileName = "StreesDownUseEffectSO", menuName = "UseEffect/StreesDown")]
public class StreesDownUseEffectSO : UseEffectBaseSO
{
    [Header("Work Down Settings")]
    [SerializeField] private AudioClip StreesDownAudioClip;
    [SerializeField] private Sprite icon;

    [Header("Event Channels")]
    [SerializeField] private TitleDataEventChannel titleDataEventChannel;
    [SerializeField] private IntEventChannel stressDownEventChannel;

    public override void ApplyEffect(GameObject target)
    {
        titleDataEventChannel.Raise(new TitleData
        {
            titleText = "- 초콜릿을 먹었습니다! -",
            subTitleText = "너무 맛있어 스트레스 수치가 감소합니다.",
            iconImage = icon,
            duration = 2.5f,
            animationDuration = 0.5f,
            audioClip = StreesDownAudioClip
        });
        stressDownEventChannel.Raise((int)Efficacy);
    }
}

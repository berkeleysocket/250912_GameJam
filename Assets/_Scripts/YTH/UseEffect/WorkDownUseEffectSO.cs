using _Scripts.Core.Events;
using _Scripts.YTH.Title;
using _Scripts.YTH.UseEffect;
using UnityEngine;

[CreateAssetMenu(fileName = "WorkDownUseEffectSO", menuName = "UseEffect/WorkDown")]
public class WorkDownUseEffectSO : UseEffectBaseSO
{
    [Header("Work Down Settings")]
    [SerializeField] private AudioClip workDownAudioClip;
    [SerializeField] private Sprite icon;

    [Header("Event Channels")]
    [SerializeField] private TitleDataEventChannel titleDataEventChannel;
    [SerializeField] private EmptyEventChannel levelDownEventChannel;

    public override void ApplyEffect(GameObject target)
    {
        titleDataEventChannel.Raise(new TitleData
        {
            titleText = "- 커피를 마셨습니다! -",
            subTitleText = "집중력이 상승해 업무 난이도가 하락합니다.",
            iconImage = icon,
            duration = 2.5f,
            animationDuration = 0.5f,
            audioClip = workDownAudioClip
        });
        levelDownEventChannel.Raise(new());
    }
}

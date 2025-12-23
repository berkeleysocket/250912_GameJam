using _Scripts.Core.Events;
using _Scripts.YTH.Title;
using Ksy.Scripts._Player;
using UnityEngine;

namespace _Scripts.YTH.UseEffect
{
    [CreateAssetMenu(fileName = "TraceUseEffectSO", menuName = "UseEffect/Trace")]
    public class TraceUseEffectSO : UseEffectBaseSO
    {
        [Header("Work Down Settings")]
        [SerializeField] private AudioClip traceAudioClip;
        [SerializeField] private Sprite icon;
        
        [Header("Event Channels")]
        [SerializeField] private Vector3EventChannel traveEventChannel;
        [SerializeField] private TitleDataEventChannel titleDataEventChannel;
        
        public override void ApplyEffect(GameObject target)
        {
            titleDataEventChannel.Raise(new TitleData
            {
                titleText = "- 흔적을 사용했습니다! -",
                subTitleText = "김부장을 혼란에 빠트릴 수 있다.",
                iconImage = icon,
                duration = 2.5f,
                animationDuration = 0.5f,
                audioClip = traceAudioClip
            });
            var player = FindAnyObjectByType<Player>();
            traveEventChannel.Raise(player.transform.position);
        }
    }
}

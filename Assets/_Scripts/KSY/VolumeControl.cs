using Ksy.Scripts.StressSystem;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Ksy.Scripts
{
    public class VolumeControl : MonoBehaviour
    {
        private Volume _volume;

        void Awake()
        {
            _volume = GetComponent<Volume>();

            if(StressManager.Instance != null)
            {
                StressManager.Instance.StressIncreased += (args)=> NarrowView();
                StressManager.Instance.StressDecreased += (args)=> WidenView();
            }
        }
        public void NarrowView()
        {
            if(_volume.profile.TryGet(out Vignette vignette))
                vignette.intensity.value += 0.1f;
            if (_volume.profile.TryGet(out ChromaticAberration ca))
                ca.intensity.value += 0.2f;
        }
        public void WidenView()
        {
            if(_volume.profile.TryGet(out Vignette vignette))
                vignette.intensity.value -= 0.1f;
            if (_volume.profile.TryGet(out ChromaticAberration ca))
                ca.intensity.value -= 0.2f;
        }
    }
}


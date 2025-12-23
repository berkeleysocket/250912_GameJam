using _Scripts.Core.Audio;
using UnityEngine;

namespace Code.UI.Setting.Volume {
    public class VolumeHandler : MonoBehaviour
    {
        [SerializeField] private AudioMixerSO mixerSO;

        public void OnValueChanged(float current) 
        {
            mixerSO.SetNormalized(current);
        }
    }
}
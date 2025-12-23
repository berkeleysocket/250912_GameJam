using System;
using _Scripts.Core.Utility;
using UnityEngine;
using UnityEngine.Audio;

namespace _Scripts.Core.Audio
{
    [CreateAssetMenu(fileName = "AudioMixerSO", menuName = "Audio/MixerSO", order = 0)]
    public class AudioMixerSO : ScriptableObject 
    {
        [field: SerializeField] public AudioMixer TargetMixer { get; private set; }
        [SerializeField] private string parameterName;

        private void OnValidate() 
        {
            if (!TargetMixer) {
                Logging.LogError($"[{name}] AudioMixer가 비어 있습니다.");
                return;
            }

            if (string.IsNullOrEmpty(parameterName)) {
                Logging.LogError($"[{name}] Mixer 파라미터 이름이 비어 있습니다.");
                return;
            }

            if (!TargetMixer.GetFloat(parameterName, out _)) {
                Logging.LogError($"[{name}] '{parameterName}' 파라미터가 Mixer에 없습니다. Exposed Parameters 확인하세요.");
                return;
            }
            
            // Logging.Log("설정 값이 유효합니다.");
        }

        [Obsolete("웬만해선 이거 대신 SetNormalized쓰세요.")]
        public bool SetFloat(float value) 
        {
            return ValidationCheck() && TargetMixer.SetFloat(parameterName, value);
        }

        /// <summary>
        /// 들어온 값을 Log10과 *20 처리 하여 데시벨에 넣어줍니다.
        /// </summary>
        /// <param name="normalized">0~1 사이의 정규화된 값입니다.</param>
        public void SetNormalized(float normalized) 
        {
            if (!ValidationCheck()) return;

            normalized = Mathf.Clamp01(normalized);
            var dB = Mathf.Log10(Mathf.Max(normalized, 0.0001f)) * 20f;
            TargetMixer.SetFloat(parameterName, dB);
        }

        public bool GetFloat(out float value) 
        {
            value = -1;
            return ValidationCheck() && TargetMixer.GetFloat(parameterName, out value);
        }

        public bool ClearFloat() 
        {
            return ValidationCheck() && TargetMixer.ClearFloat(parameterName);
        }


        private bool ValidationCheck() 
        {
            if (!TargetMixer) {
                Logging.LogError($"[{name}] Mixer가 null입니다.");
                return false;
            }

            return true;
        }
    }
}
using UnityEngine;

namespace _Scripts.YTH.UseEffect
{
    [CreateAssetMenu(fileName = "SpeedUpUseEffectSO", menuName = "UseEffect/SpeedUp")]
    public class SpeedUpUseEffectSO : UseEffectBaseSO
    {
        [field:SerializeField] public float Duration { get; private set; }
        public override void ApplyEffect(GameObject target)
        {
            Debug.Log($"Applying SpeedUp effect to {target.name} with efficacy {Efficacy}");
        }
    }
}

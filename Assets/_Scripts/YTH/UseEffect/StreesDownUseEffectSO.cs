using _Scripts.YTH.UseEffect;
using UnityEngine;

[CreateAssetMenu(fileName = "StreesDownUseEffectSO", menuName = "UseEffect/StreesDown")]
public class StreesDownUseEffectSO : UseEffectBaseSO
{
    public override void ApplyEffect(GameObject target)
    {
        Debug.Log($"Applying StreesDown effect to {target.name} with efficacy {Efficacy}");
    }
}

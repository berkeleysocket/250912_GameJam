using _Scripts.YTH.UseEffect;
using UnityEngine;

[CreateAssetMenu(fileName = "WorkDownUseEffectSO", menuName = "UseEffect/WorkDown")]
public class WorkDownUseEffectSO : UseEffectBaseSO
{
    public override void ApplyEffect(GameObject target)
    {
        Debug.Log($"Applying WorkDown effect to {target.name} with efficacy {Efficacy}");
    }
}

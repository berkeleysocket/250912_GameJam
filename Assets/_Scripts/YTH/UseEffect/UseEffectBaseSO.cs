using UnityEngine;

namespace _Scripts.YTH.UseEffect
{
    public abstract class UseEffectBaseSO : ScriptableObject
    {
        [field:SerializeField] public float Efficacy { get; private set; }

        public abstract void ApplyEffect(GameObject target);
    }
}

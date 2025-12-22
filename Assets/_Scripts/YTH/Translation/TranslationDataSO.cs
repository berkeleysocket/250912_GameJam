using UnityEngine;

namespace _Scripts.YTH.Translation
{
    [CreateAssetMenu(fileName = "TranslationDataSO", menuName = "SO/TranslationData")]
    public class TranslationDataSO : ScriptableObject
    {
        [field:SerializeField] public int Key { get; private set; }
        [field:SerializeField] public Sprite sprite { get; private set; }
    }
}

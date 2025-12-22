using System.Collections.Generic;
using _Scripts.Core.Utility;
using UnityEngine;

namespace _Scripts.YTH.Translation
{
    public class TranslationText : MonoSingleton<TranslationText>
    {
        [SerializeField] private List<TranslationDataSO> translationDatas;

        public TranslationDataSO GetRandomTranslationData()
        {
            if (translationDatas.Count == 0) return null;
            int randomIndex = Random.Range(0, translationDatas.Count);
            return translationDatas[randomIndex];
        }

        public Sprite GetTranslationSprite(int key)
        {
            foreach (var data in translationDatas)
            {
                if (data.Key == key)
                {
                    return data.sprite;
                }
            }
            return null;
        }

        public string GetTranslationToEnglish(int key)
        {
            switch (key)
            {
                case 0:
                    return "a";
                case 1:
                    return "b";
                case 2:
                    return "c";
                case 3:
                    return "d";
                case 4:
                    return "e";
                case 5:
                    return "f";
                case 6:
                    return "g";
                case 7:
                    return "h";
                case 8:
                    return "i";
                case 9:
                    return "j";
                case 10:
                    return "k";
                case 11:
                    return "l";
                case 12:
                    return "m";
                case 13:
                    return "n";
                case 14:
                    return "o";
                case 15:
                    return "p";
                case 16:
                    return "q";
                case 17:
                    return "r";
                case 18:
                    return "s";
                case 19:
                    return "t";
                case 20:
                    return "u";
                case 21:
                    return "v";
                case 22:
                    return "w";
                case 23:
                    return "x";
                case 24:
                    return "y";
                case 25:
                    return "z";
                default:
                    return "NULL";
            }
        }
    }
}

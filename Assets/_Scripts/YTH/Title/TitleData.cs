using UnityEngine;

namespace _Scripts.YTH.Title
{    
    public struct TitleData
    {
        public Sprite iconImage;
        public string titleText;
        public string subTitleText;
        public AudioClip audioClip;
        public float duration;
        public float animationDuration;

        public TitleData(Sprite image, string titleText, string subTitleText, AudioClip audioClip, float duration, float animationDuration)
        {
            this.iconImage = image;
            this.titleText = titleText;
            this.subTitleText = subTitleText;
            this.audioClip = audioClip;
            this.duration = duration;
            this.animationDuration = animationDuration;
        }
    }
}

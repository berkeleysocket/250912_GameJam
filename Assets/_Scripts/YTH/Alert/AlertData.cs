using UnityEngine;

namespace _Scripts.YTH.Alert
{    
    public struct AlertData
    {
        public string title;
        public string subTitle;
        public float duration;
        public float animationDuration;

        public AlertData(string title, string subTitle, float duration, float animationDuration)
        {
            this.title = title;
            this.subTitle = subTitle;
            this.duration = duration;
            this.animationDuration = animationDuration;
        }
    }
}

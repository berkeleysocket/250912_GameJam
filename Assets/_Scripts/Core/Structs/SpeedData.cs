using UnityEngine;

namespace _Scripts.Core.Structs
{    
    public struct SpeedData
    {
        public float Speed;
        public float Duration;

        public SpeedData(float speed, float duration)
        {
            this.Speed = speed;
            this.Duration = duration;
        }
    }
}

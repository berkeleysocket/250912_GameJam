using System;
using UnityEngine;

namespace AJ._01.Scripts.FSM
{
    public class DirectorAnimator : MonoBehaviour
    {
        public Action OnTraceEnd;
        public Action OnTraceStart;

        public void TraceEnd()
        {
            OnTraceEnd?.Invoke();    
        }
        public void TraceStart()
        {
            OnTraceStart?.Invoke();    
        }
    }
}

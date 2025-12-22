using UnityEngine;
using System;

using Ksy.Scripts.TraceSystem;

namespace Ksy.Scripts.Object
{
    public class Door : MonoBehaviour, ITraceReactive
    {
        public event Action OnOpend;

        public bool IsOpen {get; private set;}

        public void Open()
        {
            IsOpen = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
        }
        public void TraceReactiveEffect()
        {
            Open();
        }
    }
}


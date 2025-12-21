using System;
using UnityEngine;

namespace _Scripts.Core.Utility
{
    public abstract class EventChannel<T> : ScriptableObject {
        public event Action<T> OnEvent;
        public void Raise(T item) => OnEvent?.Invoke(item);
    }
}
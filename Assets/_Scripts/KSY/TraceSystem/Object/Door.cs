using UnityEngine;
using System;
using _Scripts.Core.Utility;
using Ksy.Scripts.TraceSystem;

namespace Ksy.Scripts.Object
{
    public class Door : MonoBehaviour, ITraceReactive
    {
        public GameObject GetGameObject() => gameObject;
        public event Action OnOpend;
        public event Action OnClosed;

        public bool IsOpen {get; private set;}

        public void Open()
        {
            IsOpen = true;
            GetComponent<BoxCollider2D>().isTrigger = true;
            OnOpend?.Invoke();
        }
        public void Close()
        {
            IsOpen = false;
            GetComponent<BoxCollider2D>().isTrigger = false;
            OnClosed?.Invoke();
        }


        public void Reactive()
        {
            Open();
        }
    }
}


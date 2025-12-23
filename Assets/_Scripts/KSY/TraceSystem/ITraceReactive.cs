using UnityEngine;

namespace Ksy.Scripts.TraceSystem
{
    public interface ITraceReactive
    {
        public GameObject GetGameObject();
        public void Reactive(GameObject reactor);
    }
}
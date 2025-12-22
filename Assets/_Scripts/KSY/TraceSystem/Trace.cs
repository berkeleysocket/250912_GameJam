 using System.Collections.Generic;
using UnityEngine;

using _Scripts.Core.Utility;
using UnityEngine.InputSystem;

namespace Ksy.Scripts.TraceSystem
{
    public class Trace : MonoBehaviour
    {
        public float findSize = 5f;
        public LayerMask findLayer;

        void Update()
        {
            if(Keyboard.current.spaceKey.wasPressedThisFrame)
            {
                Interaction(gameObject);
            }
        }

        public void Interaction(GameObject interactor)
        {
            var traceReactiveObj = FindInteractionObject();

            if(traceReactiveObj != null)
                traceReactiveObj.Reactive();
            else
                Logging.Log("traceReactiveObj is null");
        }
        private ITraceReactive FindInteractionObject()
        {
            Vector2 pos = transform.position;
            Vector2 size = Vector2.one * findSize; 
            float angle = 0f;

            Collider2D[] objects = Physics2D.OverlapBoxAll(pos, size, angle, findLayer);

            if(objects.Length == 0) return null;
            List<float> sortDistances = new List<float>(objects.Length);
            Dictionary<float,ITraceReactive> objectAndDistances = new Dictionary<float, ITraceReactive>();

            foreach(var obj in objects)
            {
                if(obj.transform == transform) continue;

                var dis = Vector2.Distance(transform.position, obj.transform.position);

                if(!objectAndDistances.ContainsKey(dis) && obj.TryGetComponent(out ITraceReactive sc))
                {
                    sortDistances.Add(dis);
                    objectAndDistances.Add(dis,sc);
                }
            }

            sortDistances.Sort();

            if(objectAndDistances != null && objectAndDistances.Count != 0)
            {
                float distance = sortDistances[0];

                if(objectAndDistances.ContainsKey(distance))
                {
                    var sc = objectAndDistances[distance];
                    return sc;
                }
            }
            return null;
        }
    }
}



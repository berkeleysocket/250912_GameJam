using System.Collections.Generic;
using UnityEngine;

using _Scripts.Core.Utility;

namespace Ksy.Scripts.TraceSystem
{
    public class Trace : MonoBehaviour
    {
        public float findSize = 5f;
        public LayerMask findLayer;

        public void Effect()
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
            List<float> distances = new List<float>(objects.Length);
            Dictionary<float,Collider2D> objectAndDistances = new Dictionary<float, Collider2D>();

            foreach(var obj in objects)
            {
                if(obj.transform == transform) continue;

                var dis = Vector2.Distance(transform.position, obj.transform.position);
                distances.Add(dis);
                objectAndDistances.Add(dis,obj);
            }

            distances.Sort();

            if(objectAndDistances != null && objectAndDistances.Count != 0)
            {
                float distance = distances[0];

                if(objectAndDistances.ContainsKey(distance))
                {
                    var sc = objectAndDistances[distance].GetComponent<ITraceReactive>();
                    return sc;
                }
            }
            return null;
        }
    }
}



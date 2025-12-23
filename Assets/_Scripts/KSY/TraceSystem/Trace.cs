using System;
using System.Collections.Generic;
using UnityEngine;

using DG.Tweening;

using _Scripts.Core.Structs;
using _Scripts.Core.Utility;
using AJ._01.Scripts;
using Ksy.Scripts.Object;

namespace Ksy.Scripts.TraceSystem
{
    public class Trace : MonoBehaviour
    {
        public float findSize = 5f;
        public LayerMask findLayer;
        public float distance = 2f;
        private int count = 0;
        public TraceChannel traceChannel;

        private void OnEnable()
        {
            Destroy(gameObject, 5f);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out Director director))
            {
                if (count >= 1)
                {
                    traceChannel.Raise(Empty.New);
                    Destroy(gameObject);
                }
                count++;
            }
        }

        public void Interaction(GameObject interactor)
        {
            var traceReactiveObj = FindDoor();

            if (traceReactiveObj != null)
            {
                if (count < 2)
                {
                    GetComponent<SpriteRenderer>().DOFade(0f, 0.5f);
                    Vector3 i = traceReactiveObj.GetGameObject().transform.position;
                    Vector3 j = (i - transform.position).normalized;
                    Vector2 dir = j.normalized;
                    
                    Collider2D col = traceReactiveObj.GetGameObject().GetComponent<Collider2D>();
                    Vector2 extents = col.bounds.size;
                    float moveDistance = Mathf.Abs(dir.x) > Mathf.Abs(dir.y) ? extents.x : extents.y;

                    transform.position += (Vector3)(dir * (moveDistance * distance));
                }
            }
            else
                Logging.Log("traceReactiveObj is null");
        }
        private Door FindDoor()
        {
            Vector2 pos = transform.position;
            Vector2 size = Vector2.one * findSize; 
            float angle = 0f;

            Collider2D[] objects = Physics2D.OverlapBoxAll(pos, size, angle, findLayer);
            if(objects.Length == 0) return null;
            List<float> sortDistances = new List<float>(objects.Length);
            Dictionary<float,Door> objectAndDistances = new Dictionary<float, Door>();

            foreach(var obj in objects)
            {
                if(obj.transform == transform) continue;

                var dis = Vector2.Distance(transform.position, obj.transform.position);
                if (obj.transform.parent == null) continue;
                if(!objectAndDistances.ContainsKey(dis) && obj.transform.parent.TryGetComponent(out Door sc))
                {
                    sortDistances.Add(dis);
                    objectAndDistances.Add(dis,sc);
                }
            }

            sortDistances.Sort();

            if(objectAndDistances.Count != 0)
            {
                float sortDistance = sortDistances[0];

                if(objectAndDistances.ContainsKey(sortDistance))
                {
                    var sc = objectAndDistances[sortDistance];
                    return sc;
                }
            }
            return null;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireCube(transform.position, Vector3.one * findSize);
        }
    }
}



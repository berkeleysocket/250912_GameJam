using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AJ._01.Scripts
{
    public class RandomItemOut : MonoBehaviour
    {
        [SerializeField] private List<GameObject> items;

        public GameObject Spawn()
        {
            return items[Random.Range(0, items.Count)];
        }
    }
}
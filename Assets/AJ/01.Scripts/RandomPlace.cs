using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace AJ._01.Scripts
{
    public class RandomPlace : MonoBehaviour
    {
        [SerializeField] private List<RandomItemOut> places;
        
        private void Awake()
        {
            if (places.Count == 0) GetComponentsInChildren<RandomItemOut>();
        }

        public Vector2 RandomSpawnPlace()
        {
            return places[Random.Range(0, places.Count)].Spawn().transform.position;
        }
    }
}

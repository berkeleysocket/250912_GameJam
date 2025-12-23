using System.Collections.Generic;
using UnityEngine;

namespace _Scripts.YTH.Spawner
{    
    public class RandomItemSpawner : MonoBehaviour
    {
        [Header("Spawner Settings")]
        [SerializeField] private float spawnTime = 30f;
        [SerializeField] private List<Transform> points;
        [SerializeField] private List<GameObject> gameObjects;

        private float m_time = 0f;

        private void Update()
        {
            m_time += Time.deltaTime;
            if (m_time >= spawnTime)
            {
                var item = gameObjects[Random.Range(0, gameObjects.Count)];
                Instantiate(item, points[Random.Range(0, points.Count)]);
                m_time = 0;
            }
        }
    }
}

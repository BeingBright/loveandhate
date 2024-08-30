using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private List<Transform> points = new();
        public int maxCount;
        [SerializeField] private float spawnDelay = 1f;
        [SerializeField] private bool randomSpawn;
        [SerializeField] private GameObject prefab;


        public int Count { get; private set; }


        private void Awake()
        {
            StartCoroutine(Spawn());
        }

        private IEnumerator Spawn()
        {
            for (int i = 0; i < maxCount; i++)
            {
                var obj = Instantiate(prefab, transform);
                var randIndex = 0;
                if (randomSpawn)
                {
                    randIndex = Random.Range(0, points.Count);
                }
                else
                {
                    randIndex = i % points.Count;
                }

                Count = i;
                obj.transform.position = points[randIndex].transform.position;
                yield return new WaitForSeconds(spawnDelay);
            }
        }
    }
}
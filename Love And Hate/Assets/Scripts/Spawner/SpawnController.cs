using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawner
{
    public class SpawnController : MonoBehaviour
    {
        [SerializeField] private List<Transform> points = new();
        [SerializeField] private int maxCount;
        [SerializeField] private bool randomSpawn;
        [SerializeField] private GameObject prefab;


        private int _count;


        private void Start()
        {
            for (int i = 0; i < maxCount; i++)
            {
                var obj = GameObject.Instantiate(prefab, transform);
                var randIndex = 0;
                if (randomSpawn)
                {
                    randIndex = Random.Range(0, points.Count);
                }
                else
                {
                    randIndex = i % points.Count;
                }

                obj.transform.position = points[randIndex].transform.position;
            }
        }
    }
}
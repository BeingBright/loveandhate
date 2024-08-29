using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private List<Transform> targets;

    private void Update()
    {
        var average = targets.Aggregate(new Vector3(0, 0, 0), (s, v) => s + v.position) / (float)targets.Count;
        average.z = -10;
        transform.position = average;
    }
}
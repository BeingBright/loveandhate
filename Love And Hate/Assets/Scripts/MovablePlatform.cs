using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class MovablePlatform : MonoBehaviour
{
    [FormerlySerializedAs("start")] [SerializeField]
    private Transform startTransform;

    [FormerlySerializedAs("end")] [SerializeField]
    private Transform endTransform;

    [SerializeField] private GameObject platform;

    [SerializeField] private float movementSpeed = 1f;
    [SerializeField] private bool reverse;

    private Rigidbody _rb;

    private void Awake()
    {
        _rb = platform.GetComponent<Rigidbody>();
        _rb.isKinematic = true;
    }

    private void OnValidate()
    {
        if (startTransform == null)
            startTransform = new GameObject
            {
                name = "Start Position",
                transform = { parent = transform, }
            }.transform;
        if (endTransform == null)
            endTransform = new GameObject
            {
                name = "End Position",
                transform = { parent = transform, }
            }.transform;
        if (platform == null)
        {
            platform = new GameObject
            {
                name = "Movable Platform",
                transform = { parent = transform, }
            };
        }
    }

    private void FixedUpdate()
    {
        if (reverse)
            MovePlatform(endTransform, startTransform);
        else
            MovePlatform(startTransform, endTransform);
    }

    private void MovePlatform(Transform start, Transform end)
    {
        _rb.MovePosition(Vector3.Lerp(end.position, start.position,
            (1 + Mathf.Sin(Time.timeSinceLevelLoad * movementSpeed)) * 0.5f));
    }

    private void OnDrawGizmos()
    {
        if (reverse)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(endTransform.position, 0.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(startTransform.position, 0.5f);
        }
        else
        {
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(startTransform.position, 0.5f);
            Gizmos.color = Color.red;
            Gizmos.DrawSphere(endTransform.position, 0.5f);
        }


        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(startTransform.position, endTransform.position);
    }
}
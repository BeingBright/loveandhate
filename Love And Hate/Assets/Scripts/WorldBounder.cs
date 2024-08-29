using UnityEngine;
using UnityEngine.Serialization;

public class WorldBounder : MonoBehaviour
{
    [FormerlySerializedAs("_bounds")] public Bounds bounds;

    private void Start()
    {
        bounds = FindObjectOfType<World>().bounds;
    }

    private void Update()
    {
        transform.position = bounds.ClosestPoint(transform.position);
    }
}
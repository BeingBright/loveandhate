using UnityEngine;

public class World : MonoBehaviour
{
    public Bounds bounds;

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawCube(bounds.center, bounds.size);
    }
}
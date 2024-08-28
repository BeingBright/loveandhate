using System;
using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        public Action<GameObject> onCollision;

        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("VAR");
            onCollision?.Invoke(gameObject);
        }
    }
}
using System;
using UnityEngine;

namespace Player
{
    public class Bullet : MonoBehaviour
    {
        public Action<GameObject> onCollision;

        public Side side;
        public float damage;

        private void OnCollisionEnter2D(Collision2D other)
        {
            onCollision?.Invoke(gameObject);
        }
    }
}
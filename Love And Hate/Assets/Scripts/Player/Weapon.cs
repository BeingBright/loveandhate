using System.Collections;
using UnityEngine;

namespace Player
{
    public class Weapon : MonoBehaviour
    {
        [SerializeField] private Transform barrel;
        [SerializeField] private ObjectPool pool;

        [SerializeField] private float speed = 10f;
        [SerializeField] private float rangeInSeconds = 2f;
        [SerializeField] private float damage = 2f;

        public void Fire(Side side)
        {
            var obj = pool.Get();
            obj.transform.position = barrel.position;
            obj.transform.right = -barrel.up;
            var rb = obj.GetComponent<Rigidbody2D>();
            rb.velocity = barrel.up * speed;
            StopCoroutine(Cleanup(obj));
            StartCoroutine(Cleanup(obj));

            var bullet = obj.GetComponent<Bullet>();
            bullet.side = side;
            bullet.damage = damage;
            bullet.onCollision += RemoveBullet;
        }

        private void RemoveBullet(GameObject obj)
        {
            var bullet = obj.GetComponent<Bullet>();
            bullet.onCollision -= RemoveBullet;
            pool.Destroy(obj);
        }

        private IEnumerator Cleanup(GameObject obj)
        {
            yield return new WaitForSeconds(rangeInSeconds);
            RemoveBullet(obj);
        }
    }
}
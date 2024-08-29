using System;
using Player;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;


public class NPC : MonoBehaviour
{
    private Transform _target;
    private Rigidbody2D _rb;


    [SerializeField] private float speed = 2f;
    [SerializeField] private Side side;

    public UnityEvent<Side> onSideChange;

    public void SetSide(Side newSide)
    {
        side = newSide;
        _target = side switch
        {
            Side.Cupid => GameObject.FindWithTag(Side.Devil.ToString()).transform,
            Side.Devil => GameObject.FindWithTag(Side.Cupid.ToString()).transform,
            _ => null
        };

        onSideChange?.Invoke(side);
    }


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (!_target) return;
        var dir = ((Vector2)_target.position - (Vector2)transform.position).normalized;
        transform.up = dir;
    }

    private void FixedUpdate()
    {
        if (!_target) return;
        _rb.velocity = transform.up * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (!bullet) return;
        SetSide(bullet.side);
    }
}
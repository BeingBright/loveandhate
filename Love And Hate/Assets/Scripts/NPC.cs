using Player;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class NPC : MonoBehaviour
{
    private Transform _target;
    private Rigidbody2D _rb;
    private float randSpeed;

    [SerializeField] private float distanceFromTarget = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float offset = 2f;
    [SerializeField] private AnimationCurve spread;

    private Side _side;

    public UnityEvent<Side> onSideChange;

    public void SetSide(Side newSide)
    {
        _side = newSide;
        _target = _side switch
        {
            Side.Cupid => GameObject.FindWithTag(Side.Devil.ToString()).transform,
            Side.Devil => GameObject.FindWithTag(Side.Cupid.ToString()).transform,
            _ => null
        };

        onSideChange?.Invoke(_side);
    }


    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        randSpeed = Random.Range(0f, 1f);
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
        var dist = Mathf.Clamp(Vector3.Distance(transform.position, _target.position) - distanceFromTarget, -1f, 1f);
        _rb.velocity = transform.up * ((speed + offset * spread.Evaluate(randSpeed)) * dist);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (!bullet) return;
        SetSide(bullet.side);
    }
}
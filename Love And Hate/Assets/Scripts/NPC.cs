using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;


public class NPC : MonoBehaviour
{
    private Transform _target;
    private Rigidbody2D _rb;
    private float _randSpeed;
    private bool _attacking = false;

    [SerializeField] private float attackSpeed = 2f;
    [SerializeField] private float distanceFromTarget = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float offset = 2f;
    [SerializeField] private AnimationCurve spread;

    public Side Side { get; private set; }

    public UnityEvent<Side> onSideChange;
    public UnityEvent onAttack;

    public void SetSide(Side newSide)
    {
        StopCoroutine(SetSideDelayed(Side));
        StartCoroutine(SetSideDelayed(newSide));
    }

    private IEnumerator SetSideDelayed(Side newSide)
    {
        Side = newSide;
        onSideChange?.Invoke(Side);
        _target = null;

        yield return new WaitForSeconds(1f);

        _target = Side switch
        {
            Side.Cupid => GameObject.FindWithTag(Side.Devil.ToString()).transform,
            Side.Devil => GameObject.FindWithTag(Side.Cupid.ToString()).transform,
            _ => null
        };
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _randSpeed = Random.Range(0f, 1f);
    }


    private void Update()
    {
        if (!_target) return;
        var distance = Vector3.Distance(_target.position, transform.position);
        if (distance <= distanceFromTarget && !_attacking)
        {
            _attacking = true;
            StartCoroutine(Attacking());
        }

        var dir = ((Vector2)_target.position - (Vector2)transform.position).normalized;
        transform.up = dir;
    }

    private IEnumerator Attacking()
    {
        onAttack?.Invoke();
        var player = _target.GetComponent<Controller>();
        player.Attacked();
        yield return new WaitForSeconds(attackSpeed);
        _attacking = false;
    }

    private void FixedUpdate()
    {
        if (!_target)
        {
            _rb.velocity = Vector2.zero;
            return;
        }

        if (_attacking) return;
        var dist = Mathf.Clamp(Vector3.Distance(transform.position, _target.position) - distanceFromTarget, -1f, 1f);
        _rb.velocity = transform.up * ((speed + offset * spread.Evaluate(_randSpeed)) * dist);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (!bullet) return;
        SetSide(bullet.side);
    }
}
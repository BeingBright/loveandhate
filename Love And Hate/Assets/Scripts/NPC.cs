using System;
using System.Collections;
using Player;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;
using Rigidbody2D = UnityEngine.Rigidbody2D;


public class NPC : MonoBehaviour
{
    private Transform _target;
    private Vector3 _wanderPoint;
    private float _randSpeed;
    private bool _attacking = false;
    private Rigidbody2D _rb;

    [SerializeField] private float attackSpeed = 2f;
    [SerializeField] private float attackForce = 25f;
    [SerializeField] private float distanceFromTarget = 2f;
    [SerializeField] private float speed = 2f;
    [SerializeField] private float offset = 2f;
    [SerializeField] private AnimationCurve spread;

    public Side Side { get; private set; }

    public UnityEvent<Side> onSideChange;
    public UnityEvent onAttack;
    private NavMeshAgent _agent;

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
        _randSpeed = Random.Range(0f, 1f);
        _agent = GetComponent<NavMeshAgent>();
        _agent.updateRotation = false;
        _agent.updateUpAxis = false;
        _rb = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        StartCoroutine(NewPoint());
    }

    private IEnumerator NewPoint()
    {
        while (true)
        {
            _wanderPoint = Random.insideUnitSphere * 8f;
            yield return new WaitForSeconds(4f);
        }
    }

    private void Update()
    {
        if (!_target)
        {
            _agent.SetDestination(transform.position + _wanderPoint);
        }
        else
        {
            _agent.SetDestination(_target.position);

            if (!_attacking)
            {
                var dist = Mathf.Clamp01(Vector3.Distance(transform.position, _target.position) - distanceFromTarget);
                _agent.speed = ((speed + offset * spread.Evaluate(_randSpeed)) * dist);
            }

            var distance = Vector3.Distance(_target.position, transform.position);
            if (distance <= distanceFromTarget && !_attacking)
            {
                _attacking = true;
                StartCoroutine(Attacking());
            }

            var dir = ((Vector2)_target.position - (Vector2)transform.position).normalized;
            transform.up = dir;
        }
    }

    private IEnumerator Attacking()
    {
        onAttack?.Invoke();
        var player = _target.GetComponent<Controller>();
        player.Attacked();
        _agent.velocity = -transform.up * attackForce + transform.right;

        yield return new WaitForSeconds(attackSpeed);
        _attacking = false;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        var bullet = other.gameObject.GetComponent<Bullet>();
        if (!bullet) return;
        SetSide(bullet.side);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.up);
    }
}
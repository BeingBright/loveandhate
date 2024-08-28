using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    private Vector2 _movement;
    private Vector2 _look;
    private PlayerInput _playerInput;
    private Rigidbody2D _rigidbody;

    [Range(1, 10)] [SerializeField] private float movementSpeed = 1;
    [SerializeField] private UnityEvent onFireAction;

    [SerializeField] private Transform arm;


    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        _playerInput.onActionTriggered += OnMove;
        _playerInput.onActionTriggered += OnLook;
        _playerInput.onActionTriggered += OnFire;
    }

    private void Update()
    {
        _rigidbody.velocity = (_movement * (movementSpeed));
        
        var dir = Vector2.Angle(Vector2.up, _look);
        arm.localRotation = Quaternion.Euler(new Vector3(0,0,dir));
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        if (!context.action.name.Equals("Move")) return;
        _movement = context.ReadValue<Vector2>().normalized;
    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!context.action.name.Equals("Look")) return;
        _look = context.ReadValue<Vector2>();
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        if (!context.action.name.Equals("Fire")) return;
        if (context.action.phase != InputActionPhase.Performed) return;
        onFireAction?.Invoke();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawRay(transform.position, _look);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, _movement);
    }
}
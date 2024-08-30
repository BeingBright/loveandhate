using System;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

namespace Player
{
    public class Controller : MonoBehaviour
    {
        private Vector2 _movement;
        private Vector2 _look;
        private PlayerInput _playerInput;
        private Rigidbody2D _rigidbody;

        [SerializeField] private Side side;

        [Range(1, 100)] [SerializeField] private float movementSpeed = 1;
        [SerializeField] private UnityEvent<Side> onFireAction;

        [SerializeField] private Transform arm;

        public int Health = 100;


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
            arm.up = _look.normalized;
        }

        private void FixedUpdate()
        {
            _rigidbody.velocity = (_movement * (movementSpeed));
        }

        public void OnMove(InputAction.CallbackContext context)
        {
            if (!context.action.name.Equals("Move")) return;
            _movement = context.ReadValue<Vector2>().normalized;
        }

        public void OnLook(InputAction.CallbackContext context)
        {
            if (!context.action.name.Equals("Look")) return;
            var a = context.control.device.name;
            if (a.Equals("Mouse"))
            {
                Vector2 mouseWorldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                _look = (mouseWorldPosition - (Vector2)transform.position).normalized;
            }
            else
            {
                _look = context.ReadValue<Vector2>();
            }
        }

        public void OnFire(InputAction.CallbackContext context)
        {
            if (!context.action.name.Equals("Fire")) return;
            if (context.action.phase != InputActionPhase.Performed) return;
            onFireAction?.Invoke(side);
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawRay(transform.position, _look);

            Gizmos.color = Color.red;
            Gizmos.DrawRay(transform.position, _movement);
        }

        public void Attacked()
        {
            Health -= 1;
        }

        private void OnDestroy()
        {
            _playerInput.onActionTriggered -= OnMove;
            _playerInput.onActionTriggered -= OnLook;
            _playerInput.onActionTriggered -= OnFire;
        }
    }
}
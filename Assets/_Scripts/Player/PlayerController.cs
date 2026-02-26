using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    [RequireComponent(typeof(Collider))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerInput _inputHandler;
        [SerializeField] private PlayerData _playerData;

        private float _movementSpeed = 0.4f;
        private float _mouseSpeed = 1f;
        private Rigidbody _rb;
        private CharacterController _characterController;

        private Vector3 _moveDirection;

        private bool _canMove;

        private void OnEnable()
        {
            InputReader.s_OnMoveEvent += Move;
        }

        private void OnDisable()
        {
            InputReader.s_OnMoveEvent -= Move;
        }

        private void Move(Vector2 dir)
        {
            _rb.AddForce(dir * _movementSpeed, ForceMode.Impulse);
            //_characterController.Move(dir * Time.deltaTime);
        }
        
        void Start()
        {
            _mouseSpeed = _playerData.MovementSpeed;
            _movementSpeed = _playerData.MovementSpeed;
            _rb = GetComponent<Rigidbody>();
            _characterController = GetComponent<CharacterController>();
        }
        
        
        // Update is called once per frame
        void Update()
        {
            //_characterController.Move(_moveDirection * Time.deltaTime * _movementSpeed);
        }

        public void tesst(InputAction.CallbackContext context)
        {
             Vector2 value = context.ReadValue<Vector2>();
            if (context.performed)
            {

                _moveDirection = new Vector3(value.x, 0, value.y);
            }

            if (context.canceled)
            {
                _moveDirection = Vector3.zero;
            }
        }          
    }
}

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
        private Camera _camera;

        private Vector3 _moveDirection;
        private Vector3 _movement;
        private Vector3 _rotateDirection;
        private Vector3 _lookDirection = Vector3.zero;

        private bool _isMovingForward;
        private bool _isMovingSideward;

		#region Inputs
        private void OnEnable()
        {
            InputReader.s_OnMoveEvent += Move;
            InputReader.s_OnLookEvent += Rotate;
            InputReader.s_OnUseEvent += Use;
            InputReader.s_OnInteractEvent += Interact;
        }

        private void OnDisable()
        {
            InputReader.s_OnMoveEvent -= Move;
            InputReader.s_OnLookEvent -= Rotate;
            InputReader.s_OnUseEvent -= Use;
            InputReader.s_OnInteractEvent -= Interact;
        }

		private void Move(Vector2 dir)
        {
            _moveDirection = new Vector3(dir.x * _camera.transform.forward.x, 0, dir.y * _camera.transform.forward.z).normalized;

            Debug.Log(_moveDirection);
        }

        private void Rotate(Vector2 dir)
        {
            _lookDirection.x = -dir.y;
            _lookDirection.y = dir.x;
            _rotateDirection += _lookDirection * _playerData.MouseSpeed;
        }

        private void Use() 
        {
        
        }

        private void Interact() 
        {
        
        
        }

        #endregion

        #region Unity Methods

        void Start()
        {
            _mouseSpeed = _playerData.MovementSpeed;
            _movementSpeed = _playerData.MovementSpeed;
            _rb = GetComponent<Rigidbody>();
            _camera = GetComponentInChildren<Camera>();
        }


        void FixedUpdate()
        {

            // This makes the player move
            //_rb.AddForce(_movement * _playerData.MovementSpeed);
            
            // This should probably be changed in the furture the movement keys do not follow the camera with this set up.
            _camera.transform.rotation = Quaternion.Euler(_rotateDirection.x, _rotateDirection.y, 0);

        }

        private void Reset() 
        {
            _rb = GetComponent<Rigidbody>();
            _rb.linearDamping = _playerData.MovementSpeed / 10; // Important for making the player stop again.
        }

        #endregion

	}
}

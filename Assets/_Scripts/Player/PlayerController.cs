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
        private Vector3 _movementDirection;
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
            _moveDirection.x = dir.y;
            _moveDirection.z = dir.x;
            _moveDirection.y = 0;

            //Debug.Log(_moveDirection);
        }

        private void Rotate(Vector2 dir)
        {
            _lookDirection.x = -dir.y;
            _lookDirection.y = dir.x;
            _rotateDirection += _lookDirection * _playerData.MouseSpeed;
            _rotateDirection.x = Mathf.Clamp(_rotateDirection.x, -90.0f, 90f);
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

        void Update() 
        {
            CameraRotate();
		}


        void FixedUpdate()
        {
            _rb.AddForce(Movement() * _playerData.MovementSpeed);

        }

        private void Reset() 
        {
            _rb = GetComponent<Rigidbody>();
            _rb.linearDamping = _playerData.MovementSpeed / 10; // Important for making the player stop again.
        }

		#endregion

		private Vector3 crossVector = Vector3.zero;
		private Vector3 forward = Vector3.zero;
		private Vector3 Movement() 
        {
			forward.x = _camera.transform.forward.x;
			forward.z = _camera.transform.forward.z;
			forward = forward.normalized;
			crossVector = Vector3.Cross(transform.up, forward).normalized;
			return forward * _moveDirection.x + crossVector * _moveDirection.z;
		}

        private void CameraRotate() 
        {
			_camera.transform.rotation = Quaternion.Euler(_rotateDirection.x, _rotateDirection.y, 0);
		}

	}
}

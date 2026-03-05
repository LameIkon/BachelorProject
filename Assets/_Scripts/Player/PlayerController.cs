using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(AudioSource))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;

        private Rigidbody _rb;
        private Camera _camera;

        // Used for the movement.
        private Vector3 _moveDirection;
		private Vector3 _crossVector = Vector3.zero;
		private Vector3 _forward = Vector3.zero;

        // Used for the rotation of the camera.
        private Vector3 _rotateDirection;
        private Vector3 _lookDirection = Vector3.zero;

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

        /// <summary>
        /// Inputs for the movement, assigns the directions to walk.
        /// </summary>
        /// <param name="dir">Comes from the InputReader</param>
		private void Move(Vector2 dir)
        {
            _moveDirection.x = dir.y; 
            _moveDirection.z = dir.x;
            _moveDirection.y = 0;
        }

        /// <summary>
        /// Inputs for the rotation of the camera.
        /// </summary>
        /// <param name="dir">Comes from the InputReader</param>
        private void Rotate(Vector2 dir)
        {
            _lookDirection.x = -dir.y; // These values have to be reassigned in a different way.
            _lookDirection.y = dir.x;
            _rotateDirection += _lookDirection * _playerData.MouseSpeed;
            _rotateDirection.x = Mathf.Clamp(_rotateDirection.x, -90.0f, 90f); // This ensures that the camera only goes to the top and bottom of the view.
        }

        /// <summary>
        /// Inputs for Use, Currently not assigned.
        /// </summary>
        private void Use() 
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Inputs for Interact, Currently not assigned.
        /// </summary>
        private void Interact() 
        {
            throw new NotImplementedException();
        }

        #endregion

        #region Unity Methods

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _camera = GetComponentInChildren<Camera>();
            Reset();
        }

        void Update() 
        {
            CameraRotate(); // Needs to be called here.
		}


        void FixedUpdate()
        {
            // This makes the player move.
            _rb.AddForce(Movement() * _playerData.MovementSpeed);
        }

        private void Reset() 
        {
            _rb = GetComponent<Rigidbody>();
            _rb.linearDamping = _playerData.MovementSpeed / 20; // Important for making the player stop again.
        }

        #endregion

        #region Own Method


        /// <summary>
        /// Calculates the direction of the movement and returns it.
        /// </summary>
        /// <returns>A nomalized vector of the direction.</returns>
		private Vector3 Movement() 
        {
			_forward.x = _camera.transform.forward.x;
			_forward.z = _camera.transform.forward.z;
			_forward = _forward.normalized;
            // This calculates a Vector3 that always points to the right of the forward Vector. 
			_crossVector = Vector3.Cross(transform.up, _forward).normalized;
			return _forward * _moveDirection.x + _crossVector * _moveDirection.z; // The _moveDirection can be seen as a Scalar.
		}

        /// <summary>
        /// Rotates the camera, this needs to be call in the Update. It will start to jitter if not.
        /// </summary>
        private void CameraRotate() 
        {
			_camera.transform.rotation = Quaternion.Euler(_rotateDirection.x, _rotateDirection.y, 0);
		}

		#endregion
	}
}

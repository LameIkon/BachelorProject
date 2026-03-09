using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    [RequireComponent(typeof(Collider), typeof(Rigidbody), typeof(AudioSource))]
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerData _playerData;
        [SerializeField] private float _interactDistance;

        private Rigidbody _rb;
        private Camera _camera;

        // Used for the movement.
        private Vector3 _moveDirection;
		private Vector3 _crossVector = Vector3.zero;
		private Vector3 _forward = Vector3.zero;

        // Used for the rotation of the camera.
        private Vector3 _rotateDirection;
        private Vector3 _lookDirection = Vector3.zero;

        private Vector2 MousePos { set { MousePos = value; } }

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
        /// Inputs for Use, takes the position of the mouse on the screen. 
        /// </summary>
        private void Interact(Vector2 pos) // This position could potentially be a const value, because the mouse is fixed to the middle of the screen. But it could be usefull in the future if the functionality should change.
        {
            RaycastHit hit;
            Ray ray = _camera.ScreenPointToRay(pos);

            if (Physics.Raycast(ray, out hit)) 
            {
                if (hit.collider != null) 
                {
                    hit.collider.GetComponent<IInteractable>()?.Interact();
                }
            }
        }

        /// <summary>
        /// Inputs for Use, Currently not assigned.
        /// </summary>
        private void Use() 
        {
            Debug.Log("Use");
        }

        #endregion

        #region Unity Methods

        void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _camera = GetComponentInChildren<Camera>();
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
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

using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace _Scripts
{
    [RequireComponent(typeof(Collider))]
    public class PlayerController : MonoBehaviour
    {

        [SerializeField] private PlayerData _playerData;

        private float _movementSpeed = 1f;
        private float _mouseSpeed = 1f;
        private Rigidbody _rb;

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
        }
        
        void Start()
        {
            _mouseSpeed = _playerData.MovementSpeed;
            _movementSpeed = _playerData.MovementSpeed;
            _rb = GetComponent<Rigidbody>();
        }
        
        
        // Update is called once per frame
        void Update()
        {
        
        }
    }
}

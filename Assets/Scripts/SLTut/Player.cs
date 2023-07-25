using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SLTut
{
    [RequireComponent(typeof(Controller2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f;
        
        private HareControls _hareControls;
        private Controller2D _controller2D;
        private Vector3 _playerVelocity;
        private float gravity = -20;
        private InputAction _horizontal;

        private void OnEnable()
        {
            _hareControls = new HareControls();
            _hareControls.Enable();
            _horizontal = _hareControls.FindAction("Movement");
        }

        private void Start()
        {
            _controller2D = GetComponent<Controller2D>();
        }

        private void Update()
        {
            var input = _horizontal.ReadValue<Vector2>();
            _playerVelocity.x = input.x * moveSpeed;
            _playerVelocity.y += gravity * Time.deltaTime;
            _controller2D.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
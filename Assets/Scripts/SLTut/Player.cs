using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace SLTut
{
    [RequireComponent(typeof(Controller2D))]
    public class Player : MonoBehaviour
    {
        [SerializeField] private float moveSpeed = 6f;
        [SerializeField] private float _jumpHeight = 4f;
        [SerializeField] private float _timeToJumpApex = .4f;

        private float _accelerationTimeAirborne = .2f;
        private float _accelerationGrounded = .1f;
        private float _gravity;
        private float _jumpVelocity;
        private HareControls _hareControls;
        private Controller2D _controller2D;
        private Vector3 _playerVelocity;
        private InputAction _horizontal;
        private InputAction _jump;
        private float _velocityXSmoothing;

        private void OnEnable()
        {
            _hareControls = new HareControls();
            _hareControls.Enable();
            _jump = _hareControls.FindAction("Jump");
            _horizontal = _hareControls.FindAction("Movement");
        }

        private void Start()
        {
            _controller2D = GetComponent<Controller2D>();
            _gravity = -(2 * _jumpHeight) / Mathf.Pow(_timeToJumpApex, 2);
            _jumpVelocity = Mathf.Abs(_gravity * _timeToJumpApex);
        }

        private void Update()
        {
            if (_controller2D.collisionInfo.above || _controller2D.collisionInfo.below) _playerVelocity.y = 0;

            var input = _horizontal.ReadValue<Vector2>();

            if (_jump.WasPressedThisFrame() && _controller2D.collisionInfo.below) _playerVelocity.y = _jumpVelocity;

            var targetVelocityX = input.x * moveSpeed;
            _playerVelocity.x = Mathf.SmoothDamp(_playerVelocity.x, targetVelocityX, ref _velocityXSmoothing,
                _controller2D.collisionInfo.below ? _accelerationGrounded : _accelerationTimeAirborne);
            _playerVelocity.y += _gravity * Time.deltaTime;
            _controller2D.Move(_playerVelocity * Time.deltaTime);
        }
    }
}
﻿using UnityEngine;
using UnityEngine.InputSystem;

public class GptPlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;
    [SerializeField] private float slopeCheckDistance;
    [SerializeField] private LayerMask groundLayerMask;
    [SerializeField] private float groundCheckRadius;
    [SerializeField] private Transform groundCheckTransform;
    [SerializeField] private PhysicsMaterial2D noFriction;
    [SerializeField] private PhysicsMaterial2D fullFriction;
    [SerializeField] private float maxSlopeAngle;
    [SerializeField] private float groundSmoothTime = 0.1f;
    [SerializeField] private float slopeSmoothTime = 0.001f;
    [SerializeField] private RotateOnSlopeHelper rotateOnSlopeHelper;

    // Moving & Jumping Variables
    private CapsuleCollider2D _capsuleCollider2D;
    private HareControls _hareActionMap;
    private InputAction _horizontal;
    private Rigidbody2D _rigidbody;
    private Vector2 _desiredVelocity;
    private float _xInput;
    private Vector2 _colliderSize;
    private bool _isGrounded;
    private bool _isJumping;
    private bool _canJump = true;
    private int _facingDirection = 1;
    private Animator _animator;

    // Slope Variables
    private float _slopeDownAngle;
    private Vector2 _slopeNormalPerpendicular;
    private bool _isOnSlope;
    private bool _canWalkOnSlope;
    private float _slopeDownAngleOld;
    private float _slopeSideAngle;
    private float _spriteRotateAngle;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");

    private void OnEnable()
    {
        _hareActionMap = new HareControls();
        _hareActionMap.Enable();
        _horizontal = _hareActionMap.HareMovement.Movement;
        _hareActionMap.HareMovement.Jump.performed += HandleJump;
    }

    private void Start()
    {
        _capsuleCollider2D = GetComponent<CapsuleCollider2D>();
        _colliderSize = _capsuleCollider2D.size;
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        CheckHorizontalInput();
    }

    private void OnDisable()
    {
        _hareActionMap.HareMovement.Jump.performed -= HandleJump;
    }

    private void CheckHorizontalInput()
    {
        _xInput = _horizontal.ReadValue<Vector2>().x;
        if (Mathf.Abs(_xInput) > 0)
        {
            _animator.SetBool(IsRunning, true);
        }
        else
        {
            _animator.SetBool(IsRunning, false);
        }
        if (_xInput == 1 && _facingDirection == -1)
            Flip();
        else if (_xInput == -1 && _facingDirection == 1) Flip();
    }

    private void Flip()
    {
        _facingDirection *= -1;
        transform.Rotate(0.0f, 180.0f, 0.0f);
    }

    private void FixedUpdate()
    {
        CheckGrounded();
        SlopeCheck();
        ApplyMovement();
        if(rotateOnSlopeHelper)
            rotateOnSlopeHelper.RotateOnSlope(_facingDirection, -_spriteRotateAngle);
    }

    private void ApplyMovement()
    {
        var currentVelocity = Vector2.zero;

        if (_isGrounded && !_isOnSlope)
            _desiredVelocity.Set(moveSpeed * _xInput, _rigidbody.velocity.y);
        else if (_isGrounded && _isOnSlope && _canWalkOnSlope)
            _desiredVelocity.Set(moveSpeed * _slopeNormalPerpendicular.x * -_xInput, _rigidbody.velocity.y);
        else if (!_isGrounded) _desiredVelocity.Set(moveSpeed * _xInput, _rigidbody.velocity.y);

        // Separate the horizontal and vertical velocity handling
        var newX = Mathf.SmoothDamp(_rigidbody.velocity.x, _desiredVelocity.x, ref currentVelocity.x,
            _isOnSlope ? slopeSmoothTime : groundSmoothTime);

        // Don't manipulate y velocity when jumping
        var newY = _isJumping
            ? _rigidbody.velocity.y
            : Mathf.SmoothDamp(_rigidbody.velocity.y, _desiredVelocity.y, ref currentVelocity.y,
                _isOnSlope ? slopeSmoothTime : groundSmoothTime);

        _rigidbody.velocity = new Vector2(newX, newY);
    }

    private void CheckGrounded()
    {
        _isGrounded = Physics2D.OverlapCircle(groundCheckTransform.position, groundCheckRadius, groundLayerMask);

        if (_rigidbody.velocity.y <= 0.0f) _isJumping = false;

        if (_isGrounded && !_isJumping && _slopeDownAngle <= maxSlopeAngle) _canJump = true;
    }

    private void HandleJump(InputAction.CallbackContext obj)
    {
        // Double Jump
        // Float to apex and increase fall speed
        if (_canJump)
        {
            _canJump = false;
            _isJumping = true;
            if (_isOnSlope)
            {
                _desiredVelocity.Set(0f, 0f);
                _rigidbody.velocity = _desiredVelocity;    
            }
            /*var jumpDirection = _isOnSlope
                ? Vector2.Lerp(_slopeNormalPerpendicular, Vector2.up, jumpDirectionLerpValue)
                : Vector2.up;
            Debug.Log("Jump Dir: " + jumpDirection);*/
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }

    private void SlopeCheck()
    {
        var checkPosition = transform.position - new Vector3(0f, _colliderSize.y / 2);
        HorizontalSlopeCheck(checkPosition);
        VerticalSlopeCheck(checkPosition);
    }

    private void HorizontalSlopeCheck(Vector2 checkPosition)
    {
        var slopeHitFront = Physics2D.Raycast(checkPosition, transform.right, slopeCheckDistance, groundLayerMask);
        Debug.DrawRay(checkPosition, transform.right * slopeCheckDistance, Color.red);
        var slopeHitBack = Physics2D.Raycast(checkPosition, -transform.right, slopeCheckDistance, groundLayerMask);
        Debug.DrawRay(checkPosition, -transform.right * slopeCheckDistance, Color.red);

        //Debug.Log($"Front: {slopeHitFront.normal}, Back: {slopeHitBack.normal}");
        
        if (slopeHitFront)
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack)
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitBack.normal, Vector2.up);
        }
        else
        {
            _slopeSideAngle = 0f;
            _isOnSlope = false;
        }
    }

    private void VerticalSlopeCheck(Vector2 checkPosition)
    {
        var hit = Physics2D.Raycast(checkPosition, Vector2.down, slopeCheckDistance, groundLayerMask);

        if (hit)
        {
            Debug.DrawRay(hit.point, hit.normal * 2f, Color.green);
            _slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);
            _spriteRotateAngle = Vector2.SignedAngle(hit.normal, Vector2.up);

            if (_slopeDownAngle != _slopeDownAngleOld) _isOnSlope = true;

            _slopeDownAngleOld = _slopeDownAngle;
        }

        if ((_slopeDownAngle > maxSlopeAngle || _slopeSideAngle > maxSlopeAngle) && _slopeDownAngle != 0)
            _canWalkOnSlope = false;
        else
            _canWalkOnSlope = true;

        if (_isOnSlope && _canWalkOnSlope && _xInput == 0f && (_slopeDownAngle != 0 && _slopeSideAngle != 90))
            _rigidbody.sharedMaterial = fullFriction;
        else
            _rigidbody.sharedMaterial = noFriction;
    }
}
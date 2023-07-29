using UnityEngine;
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
    [SerializeField] private float wallAngle = 80f; // The angle beyond which an object is considered a wall
    [SerializeField] private float groundSmoothTime = 0.1f;
    [SerializeField] private float slopeSmoothTime = 0.001f;
    [SerializeField] private float jumpDirectionLerpValue = 0.5f;

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

    // Slope Variables
    private float _slopeDownAngle;
    private Vector2 _slopeNormalPerpendicular;
    private bool _isOnSlope;
    private bool _canWalkOnSlope;
    private float _slopeDownAngleOld;
    private float _slopeSideAngle;

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
        if (_canJump)
        {
            _canJump = false;
            _isJumping = true;

            var jumpDirection = _isOnSlope
                ? Vector2.Lerp(_slopeNormalPerpendicular, Vector2.up, jumpDirectionLerpValue)
                : Vector2.up;
            Debug.Log("Jump Dir: " + jumpDirection);
            _rigidbody.AddForce(jumpDirection * jumpForce, ForceMode2D.Impulse);
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
        var slopeHitBack = Physics2D.Raycast(checkPosition, -transform.right, slopeCheckDistance, groundLayerMask);

        if (slopeHitFront && Vector2.Angle(slopeHitFront.normal, Vector2.up) < wallAngle)
        {
            _isOnSlope = true;
            _slopeSideAngle = Vector2.Angle(slopeHitFront.normal, Vector2.up);
        }
        else if (slopeHitBack && Vector2.Angle(slopeHitBack.normal, Vector2.up) < wallAngle)
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
            _slopeNormalPerpendicular = Vector2.Perpendicular(hit.normal).normalized;
            _slopeDownAngle = Vector2.Angle(hit.normal, Vector2.up);

            if (_slopeDownAngle != _slopeDownAngleOld) _isOnSlope = true;

            _slopeDownAngleOld = _slopeDownAngle;

            _canWalkOnSlope = _slopeDownAngle <= maxSlopeAngle;

            if (_isOnSlope && _canWalkOnSlope && _xInput == 0f)
                _rigidbody.sharedMaterial = fullFriction;
            else if (_isOnSlope && _canWalkOnSlope && _xInput != 0f)
                _rigidbody.sharedMaterial = noFriction;
            else
                _rigidbody.sharedMaterial = fullFriction;
        }

        if (_isOnSlope && _canWalkOnSlope && _xInput == 0f)
            _rigidbody.sharedMaterial = fullFriction;
        else
            _rigidbody.sharedMaterial = noFriction;
    }
}
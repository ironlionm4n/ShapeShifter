using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class BasePlayerController : MonoBehaviour
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
    [SerializeField] private Transform slopeCheckTransform;
    [SerializeField] private FacingDirection _facingDirection = FacingDirection.Right;
    [SerializeField] private GameObject hareRig;
    [SerializeField] private Animator animator;


    // Moving & Jumping Variables
    private Vector3 _checkPosition;
    private HareControls _hareActionMap;
    private InputAction _horizontal;
    private Rigidbody2D _rigidbody;
    private Vector2 _desiredVelocity;
    private float _xInput;
    private bool _isGrounded;
    private bool _isJumping;

    // Slope Variables
    private float _slopeDownAngle;
    private Vector2 _slopeNormalPerpendicular;
    private bool _isOnSlope;
    private bool _canWalkOnSlope;
    private float _slopeDownAngleOld;
    private float _slopeSideAngle;
    private float _spriteRotateAngle;
    private static readonly int IsRunning = Animator.StringToHash("isRunning");
    private bool _hasDoubleJumped;
    [SerializeField] private float fallingGravityModifier = 4f;
    [SerializeField] private float jumpingGravityModifier = 2f;
    private int _jumpCount = 0;

    private void OnEnable()
    {
        _hareActionMap = new HareControls();
        _hareActionMap.Enable();
        _horizontal = _hareActionMap.HareMovement.Movement;
    }

    private void Start()
    {
        _checkPosition = slopeCheckTransform.position;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        Debug.Log("jump count: "+_jumpCount);
        CheckHorizontalInput();
        // Custom gravity: Increase fall speed after reaching the apex of the jump
        if (_rigidbody.velocity.y < 0)
        {
            _rigidbody.velocity += Vector2.up * (Physics2D.gravity.y * (fallingGravityModifier * Time.deltaTime));
        }
        else
        {
            _rigidbody.velocity += Vector2.up * (Physics2D.gravity.y * (jumpingGravityModifier * Time.deltaTime));
        }
        if (_hareActionMap.HareMovement.Jump.WasPerformedThisFrame() && _jumpCount < 1)
        {
            HandleJump();
        }
    }

    private void CheckHorizontalInput()
    {
        _xInput = _horizontal.ReadValue<Vector2>().x;
        if (Mathf.Abs(_xInput) > 0)
            animator.SetBool(IsRunning, true);
        else
            animator.SetBool(IsRunning, false);
        if (_xInput < 0 && _facingDirection != FacingDirection.Left)
            FlipLeft();
        else if (_xInput > 0 && _facingDirection != FacingDirection.Right) FlipRight();
    }

    private void FlipLeft()
    {
        _facingDirection = FacingDirection.Left;
        transform.localScale = new Vector3(-1, 1, 1);
    }

    private void FlipRight()
    {
        _facingDirection = FacingDirection.Right;
        transform.localScale = new Vector3(1, 1, 1);
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

        //if (_rigidbody.velocity.y <= 0.0f) _isJumping = false;

        if (_isGrounded && _slopeDownAngle <= maxSlopeAngle)
        {
            _isJumping = false;
            _jumpCount = 0;
            _hasDoubleJumped = false;
        }
    }

    private void HandleJump()
    {
        // If the player is on the ground or hasn't double jumped yet
        if (!_isJumping || !_hasDoubleJumped)
        {
            _jumpCount++;
            // Reset vertical velocity for consistent jump height
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, 0f);

            // Apply jump force
            _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);

            // If the player is already in the air (i.e., this is a double jump)
            if (_isJumping)
            {
                _hasDoubleJumped = true;
            }

            _isJumping = true;
        }
        // Double Jump
        // Float to apex and increase fall speed
        /*_isJumping = true;
        if (_isOnSlope)
        {
            _desiredVelocity.Set(0f, 0f);
            _rigidbody.velocity = _desiredVelocity;
        }
        
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);*/
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.magenta;
        Gizmos.DrawWireSphere(groundCheckTransform.position, groundCheckRadius);
    }

    private void SlopeCheck()
    {
        _checkPosition = slopeCheckTransform.position;
        // var checkPosition = transform.position - new Vector3(0f, _colliderSize.y / 2);
        HorizontalSlopeCheck(_checkPosition);
        VerticalSlopeCheck(_checkPosition);
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
//            Debug.Log(_spriteRotateAngle);

            if (_slopeDownAngle != _slopeDownAngleOld) _isOnSlope = true;

            _slopeDownAngleOld = _slopeDownAngle;
        }

        if ((_slopeDownAngle > maxSlopeAngle || _slopeSideAngle > maxSlopeAngle) && _slopeDownAngle != 0)
            _canWalkOnSlope = false;
        else
            _canWalkOnSlope = true;

        if (_isOnSlope && _canWalkOnSlope && _xInput == 0f && _slopeDownAngle != 0 && _slopeSideAngle != 90)
            _rigidbody.sharedMaterial = fullFriction;
        else
            _rigidbody.sharedMaterial = noFriction;
    }
}

internal enum FacingDirection
{
    Left,
    Right
}
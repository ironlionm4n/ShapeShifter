using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float jumpForce;

    private HareControls _hareActionMap;
    private InputAction _horizontal;
    private Rigidbody2D _rigidbody;
    private float _direction;

    private void OnEnable()
    {
        _hareActionMap = new HareControls();
        _hareActionMap.Enable();
        _horizontal = _hareActionMap.HareMovement.Movement;
        _hareActionMap.HareMovement.Jump.performed += HandleJump;
    }

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _direction = _horizontal.ReadValue<float>();
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = new Vector2(_direction * moveSpeed, _rigidbody.velocity.y);
    }

    private void OnDisable()
    {
        _hareActionMap.HareMovement.Jump.performed -= HandleJump;
    }

    private void HandleJump(InputAction.CallbackContext obj)
    {
        _rigidbody.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    public static Vector2 Movement;

    private PlayerInput _playerInput;
    private InputAction _moveAction;
    private InputAction _attackAction;
    private InputAction _interactAction;
    public static bool _isAttacking;
    public static bool _isInteracting;

    private void Awake()
    {
        _playerInput = GetComponent<PlayerInput>();

        _moveAction = _playerInput.actions["Move"];

        _attackAction = _playerInput.actions["Attack"];

        _interactAction = _playerInput.actions["Interact"];
    }

    private void Update()
    {
        Movement = _moveAction.ReadValue<Vector2>();

        if (_attackAction.WasPressedThisFrame())
        {
            _isAttacking = true;
        }
        if (_attackAction.WasReleasedThisFrame())
        {
            _isAttacking = false;
        }
        if (_interactAction.WasPressedThisFrame())
        {
            _isInteracting = true;
        }
        if (_interactAction.WasReleasedThisFrame())
        {
            _isInteracting = false;
        }
    }
}

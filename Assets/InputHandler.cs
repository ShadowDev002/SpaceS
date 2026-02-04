using System;
using NUnit.Framework;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] public Vector2 Look { get; private set; }
    public bool IsFiring { get; private set; }
    public bool IsSprinting { get; private set; }
    public event Action OnSkill_1;
    public event Action OnPause;

    private InputSystem_Actions _inputActions;

    void Awake()
    {
        _inputActions = new InputSystem_Actions();
        _inputActions.Player.Look.performed += ctx => Look = ctx.ReadValue<Vector2>();

        _inputActions.Player.Attack.started += ctx => IsFiring = true;
        _inputActions.Player.Attack.canceled += ctx => IsFiring = false;

        _inputActions.Player.Sprint.started += ctx => IsSprinting = true;
        _inputActions.Player.Sprint.canceled += ctx => IsSprinting = false;

        _inputActions.Player.Skill_1.performed += ctx => OnSkill_1?.Invoke();
        _inputActions.Player.Pause.performed += ctx => OnPause?.Invoke();
    }

    void OnEnable()
    {
        _inputActions.Player.Enable();
    }
    void OnDisable()
    {
        _inputActions.Player.Disable();
    }
}

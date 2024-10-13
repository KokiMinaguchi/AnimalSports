using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.InputSystem;

internal sealed class PlayerInputProvider : MonoBehaviour, IPlayerInputProvider
{
    public PlayerInput InputAction => _inputAction;
    public ReadOnlyReactiveProperty<bool> Jump => _jump;
    public ReadOnlyReactiveProperty<Vector2> Move => _move;
    public ReadOnlyReactiveProperty<bool> NormalAttack => _normalAttack;
    public ReadOnlyReactiveProperty<bool> SpecialAttack => _specialAttack;

    private PlayerInput _inputAction;
    private readonly ReactiveProperty<Vector2> _move = new();
    private readonly ReactiveProperty<bool> _jump = new();
    private readonly ReactiveProperty<bool> _normalAttack = new();
    private readonly ReactiveProperty<bool> _specialAttack = new();
    
    // Start is called before the first frame update
    void Awake()
    {
        _inputAction = new PlayerInput();
        _inputAction.Game.Walk.performed += OnWalk;
        _inputAction.Game.Walk.canceled += OnWalk;
        _inputAction.Game.Jump.performed += OnJump;
        _inputAction.Game.Jump.canceled += OnJumpStop;
        _inputAction.Game.Attack.performed += OnNormalAttack;
        _inputAction.Game.Attack.canceled += OnNormalAttackStop;
        _inputAction.AddTo(this);
        _inputAction.Enable();

        _move.AddTo(this);
        _jump.AddTo(this);
        _normalAttack.AddTo(this);
        _specialAttack.AddTo(this);
    }

    void OnDestroy()
    {
        _move.Dispose();
        _jump.Dispose();
        _normalAttack.Dispose();
        _specialAttack.Dispose();

        _inputAction.Disable();
        _inputAction.Dispose();
    }

    private void OnWalk(InputAction.CallbackContext context)
    {
        _move.Value = context.ReadValue<Vector2>();
        Debug.Log("WalkValue:" + context.ReadValue<Vector2>());
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        _jump.Value = true;
    }
    private void OnJumpStop(InputAction.CallbackContext context)
    {
        _jump.Value = false;
    }
    private void OnNormalAttack(InputAction.CallbackContext context)
    {
        _normalAttack.Value = true;
        Debug.Log(context.phase);
    }

    private void OnNormalAttackStop(InputAction.CallbackContext context)
    {
        _normalAttack.Value = false;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤー入力提供クラス
/// </summary>
internal sealed class PlayerInputProvider : MonoBehaviour, IPlayerInputProvider
{
    #region Property

    public PlayerInput InputAction => _inputAction;
    public ReadOnlyReactiveProperty<bool> Jump => _jump;
    public ReadOnlyReactiveProperty<Vector2> Move => _move;

    public ReadOnlyReactiveProperty<bool> ClickAimTarget => _clickAimTarget;
    public ReadOnlyReactiveProperty<bool> Fire => _fire;
    public ReadOnlyReactiveProperty<bool> NormalAttack => _normalAttack;
    public ReadOnlyReactiveProperty<bool> SpecialAttack => _specialAttack;
    public ReadOnlyReactiveProperty<bool> OpenMenu => _openMenu;
    public ReadOnlyReactiveProperty<bool> OpenMap => _openMap;

    #endregion

    #region Field

    private PlayerInput _inputAction;
    private readonly ReactiveProperty<Vector2> _move = new();
    private readonly ReactiveProperty<bool> _jump = new(false);
    private readonly ReactiveProperty<bool> _clickAimTarget = new(false);
    private readonly ReactiveProperty<bool> _fire = new(false);
    private readonly ReactiveProperty<bool> _normalAttack = new();
    private readonly ReactiveProperty<bool> _specialAttack = new();

    private readonly ReactiveProperty<bool> _openMenu = new(false);
    private readonly ReactiveProperty<bool> _openMap = new(false);

    #endregion

    #region Unity

    // Start is called before the first frame update
    void Awake()
    {
        _inputAction = new PlayerInput();
        
        _inputAction.Game.Walk.performed += OnWalk;
        _inputAction.Game.Walk.canceled += OnWalk;
        _inputAction.Game.Jump.performed += OnJump;
        _inputAction.Game.Jump.canceled += OnJumpStop;
        _inputAction.Game.ClickAimTarget.performed += OnClickAimTarget;
        _inputAction.Game.ClickAimTarget.canceled += OnClickAimTargetStop;
        _inputAction.Game.Fire.performed += OnFire;
        _inputAction.Game.Fire.canceled += OnFireStop;
        _inputAction.Game.Attack.performed += OnNormalAttack;
        _inputAction.Game.Attack.canceled += OnNormalAttackStop;

        _inputAction.Game.OpenMenu.performed += OnOpenMenu;
        _inputAction.Game.OpenMenu.canceled += OnOpenMenu;
        _inputAction.Game.OpenMap.performed += OnOpenMap;
        _inputAction.Game.OpenMap.canceled += OnOpenMap;

        //_inputAction.UI.Click.performed += OnClick;
        //_inputAction.UI.Click.

        _inputAction.AddTo(this);
        _inputAction.Enable();

        _move.AddTo(this);
        _jump.AddTo(this);
        _clickAimTarget.AddTo(this);
        _fire.AddTo(this);
        _normalAttack.AddTo(this);
        _specialAttack.AddTo(this);
        _openMenu.AddTo(this);
        _openMap.AddTo(this);
    }

    void OnDestroy()
    {
        _inputAction.Game.Walk.performed -= OnWalk;
        _inputAction.Game.Walk.canceled -= OnWalk;
        _inputAction.Game.Jump.performed -= OnJump;
        _inputAction.Game.Jump.canceled -= OnJumpStop;
        _inputAction.Game.ClickAimTarget.performed -= OnClickAimTarget;
        _inputAction.Game.ClickAimTarget.canceled -= OnClickAimTargetStop;
        _inputAction.Game.Fire.performed -= OnFire;
        _inputAction.Game.Fire.canceled -= OnFireStop;
        _inputAction.Game.Attack.performed -= OnNormalAttack;
        _inputAction.Game.Attack.canceled -= OnNormalAttackStop;
        _inputAction.Game.OpenMenu.performed -= OnOpenMenu;
        _inputAction.Game.OpenMenu.canceled -= OnOpenMenu;
        _inputAction.Game.OpenMap.performed -= OnOpenMap;
        _inputAction.Game.OpenMap.canceled -= OnOpenMap;
        
        _move.Dispose();
        _jump.Dispose();
        _clickAimTarget.Dispose();
        _fire.Dispose();
        _normalAttack.Dispose();
        _specialAttack.Dispose();
        _openMenu.Dispose();
        _openMap.Dispose();

        _inputAction.Disable();
        _inputAction.Dispose();
    }

    #endregion

    #region Method

    private void OnWalk(InputAction.CallbackContext context)
    {
        _move.Value = context.ReadValue<Vector2>();
        //Debug.Log("WalkValue:" + context.ReadValue<Vector2>());
    }
    private void OnJump(InputAction.CallbackContext context)
    {
        _jump.Value = true;
    }
    private void OnJumpStop(InputAction.CallbackContext context)
    {
        _jump.Value = false;
    }
    private void OnClickAimTarget(InputAction.CallbackContext context)
    {
        _clickAimTarget.Value = true;
    }
    private void OnClickAimTargetStop(InputAction.CallbackContext context)
    {
        _clickAimTarget.Value = false;
    }
    private void OnFire(InputAction.CallbackContext context)
    {
        _fire.Value = _inputAction.Game.Fire.IsPressed();
    }
    private void OnFireStop(InputAction.CallbackContext context)
    {
        _fire.Value = _inputAction.Game.Fire.IsPressed();
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

    private void OnOpenMenu(InputAction.CallbackContext context)
    {
        _openMenu.Value = _inputAction.Game.OpenMenu.IsPressed();
    }
    private void OnOpenMap(InputAction.CallbackContext context)
    {
        _openMap.Value = _inputAction.Game.OpenMap.IsPressed();
    }

    private void OnClick(InputAction.CallbackContext context)
    {

    }

    #endregion
}

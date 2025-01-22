using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

/// <summary>
/// プレイヤーの入力提供クラスのインターフェイス
/// </summary>
public interface IPlayerInputProvider
{
    #region Property

    public PlayerInput InputAction { get; }
    public ReadOnlyReactiveProperty<Vector2> Move {  get; }
    public ReadOnlyReactiveProperty<bool> Jump { get; }
    public ReadOnlyReactiveProperty<bool> ClickAimTarget { get; }
    public ReadOnlyReactiveProperty<bool> Fire { get; }
    public ReadOnlyReactiveProperty<bool> NormalAttack { get; }
    public ReadOnlyReactiveProperty<bool> SpecialAttack { get; }
    public ReadOnlyReactiveProperty<bool> OpenMap { get; }

    #endregion
}

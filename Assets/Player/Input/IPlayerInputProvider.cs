using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

public interface IPlayerInputProvider
{
    public PlayerInput InputAction { get; }
    public ReadOnlyReactiveProperty<Vector2> Move {  get; }
    public ReadOnlyReactiveProperty<bool> Jump { get; }
    public ReadOnlyReactiveProperty<bool> NormalAttack { get; }
    public ReadOnlyReactiveProperty<bool> SpecialAttack { get; }
}

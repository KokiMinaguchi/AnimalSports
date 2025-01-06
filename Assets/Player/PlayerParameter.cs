using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerParameter : MonoBehaviour
{
    [SerializeField]
    private bool _isGround = false;
    public bool IsGround {  get { return _isGround; } set { _isGround = value; } }
}

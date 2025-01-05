using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

/// <summary>
/// 吸い込み可能オブジェクト
/// </summary>
[RequireComponent(typeof(Rigidbody))]
public class InholeObject : MonoBehaviour
{
    public bool _isInhole = false;
    private Rigidbody _rb;
    [SerializeField]
    private Transform _playerPos;

    public float _accel;

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_isInhole == true)
        {
            _accel += 0.1f;
            _rb.AddForce((_playerPos.position - transform.position).normalized * _accel, ForceMode.Force);
        }
    }
}

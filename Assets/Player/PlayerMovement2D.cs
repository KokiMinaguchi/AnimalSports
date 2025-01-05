using R3;
using R3.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

/// <summary>
/// プレイヤー移動クラス
/// </summary>
[RequireComponent(typeof(PlayerInputProvider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement2D : MonoBehaviour
{
    #region SerializeField

    [SerializeField]
    [Header("歩く速さ")]
    private float _walkSpeed;

    [SerializeField]
    [Header("加速度")]
    private Vector3 _accel;

    [SerializeField, Range(1f, 20f)]
    [Header("ジャンプ力")]
    private float _jumpPower;

    [SerializeField]
    [Header("移動速度の入力に対する追従度")]
    private float _moveTracking;

    #endregion

    #region Field

    private PlayerInputProvider _inputProvider;
    private Rigidbody _rb;

    #endregion


    private bool _dash;


    float _dragSpeed;

    private float _targetRotation = 0.0f;
    public float RotationSmoothTime = 0.12f;
    GameObject _mainCamera;

    [SerializeField]
    private float _dashSpeed;
    private float _speed;
    private float _rotationVelocity;
    private float _verticalVelocity;



    [SerializeField]
    [Header("カメラが見る位置")]
    private GameObject _aimTarget;

    public bool _isGround;

    [SerializeField]
    private Vector3 _localGravity;

    Vector3 _inputDirection;
    Vector3 _targetDirection;
    Vector3 _moveVelocity;

    #region Unity

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _rb = GetComponent<Rigidbody>();
        _inputProvider = GetComponent<PlayerInputProvider>();
        _inputDirection = Vector3.zero;
        Physics.gravity = _localGravity;

        // デバッグテスト
        //_inputProvider.Inhole.Subscribe(value => { Debug.Log("吸い込み" + value); }).AddTo(this);

        // 移動キーを入力していないとき
        _inputProvider.Move
            .Where(value => value.magnitude <= 0.4f)
            .Subscribe(_ => 
            { 
                _speed = 0f;
                Debug.Log("STOP");
                //_rb.AddForce(-_rb.velocity / Time.fixedDeltaTime);
                _dragSpeed = 5f;
               _rb.drag = _dragSpeed;
                
            })
            .AddTo(this);

        // 移動キーを入力しているとき
        _inputProvider.Move
            .Where(value => value.magnitude > 0.5f)
            .Subscribe(_ =>
            {
                _speed = _walkSpeed;
                Debug.Log("MOVE");
                _dragSpeed = 0.0f;
                _rb.drag = _dragSpeed;
                //PlayerRotation();
            })
            .AddTo(this);

        // ジャンプ
        _inputProvider.Jump
            .Where(_ => _isGround == true)
            .Subscribe(_ =>
            {
                Debug.Log("JUMP");
                _verticalVelocity = Mathf.Sqrt(_jumpPower * -2f * _localGravity.y);
                //_rb.AddForce(Vector3.up * _verticalVelocity, ForceMode.Impulse);
                _isGround = false;
            })
            .AddTo(this);

        // 更新処理
        this.FixedUpdateAsObservable()
            .Subscribe(_ =>
            {
                //if(_inputProvider.Move.CurrentValue.magnitude > 0.5f)
                //{
                //    _rb.drag = 0.0f;
                //}
                //else
                //{
                //    _rb.drag = 5f;
                //}

                
                _inputDirection = 
                new Vector3(_inputProvider.Move.CurrentValue.x, 0, _inputProvider.Move.CurrentValue.y).normalized;
                
                float targetSpeed = _dash ? _dashSpeed : _walkSpeed;

                if (_inputProvider.Move.CurrentValue != Vector2.zero)
                {
                    PlayerRotation();
                }

                _targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
                
                //_moveVelocity = _targetDirection.normalized * _speed;
                _moveVelocity = _inputDirection * _speed;
                if (_isGround == false)
                {
                    //_rb.AddForce(_accel, ForceMode.Acceleration);
                    //_moveVelocity = _moveVelocity * _accel.x;
                    //finalSpeed = finalSpeed * _accel.x;
                    //_rb.AddForce(_targetDirection.normalized * factor, ForceMode.Acceleration);
                    //_rb.AddForce(_localGravity, ForceMode.Acceleration);
                    _verticalVelocity += _localGravity.y * Time.fixedDeltaTime;
                }
                
                //  滑る移動
                //_rb.AddForce(new Vector3(_moveVelocity.x, _verticalVelocity, _moveVelocity.z), ForceMode.Force);

                //Vector3 finalSpeed = _moveTracking * (_moveVelocity - _rb.velocity);

                //Debug.Log("FinalSpeed:"+ finalSpeed);


                //_verticalVelocity = _moveTracking * (_verticalVelocity - _rb.velocity.y);
                //_rb.AddForce(new Vector3(_moveVelocity.x, _verticalVelocity, _moveVelocity.z), ForceMode.Force);
                //Vector3 flatVal = new Vector3(_rb.velocity.x, 0, _rb.velocity.z);
                //Debug.Log(flatVal);
                //if (flatVal.magnitude > _walkSpeed)
                //{
                //    _rb.velocity = new Vector3(flatVal.x, _rb.velocity.y, flatVal.z);
                //}
                //Debug.Log(_rb.velocity);
                // すべらない移動
                _rb.velocity = new Vector3(_moveVelocity.x, _verticalVelocity, _moveVelocity.z);
                
            });
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGround = true;
        }
    }

    #endregion

    #region Method

    public void OnDash(InputAction.CallbackContext context)
    {
        switch(context.phase)
        {
            case InputActionPhase.Started:
                _dash = true;
                break;
            case InputActionPhase.Canceled:
                _dash = false;
                break;
            default:
                break;
        }
        Debug.Log(_dash);
    }

    private void PlayerRotation()
    {
        _targetRotation = Mathf.Atan2(_inputDirection.x, _inputDirection.z) * Mathf.Rad2Deg;// +
                                                                                          //_mainCamera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
            RotationSmoothTime);

        // rotate to face input direction relative to camera position
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }

    #endregion
}

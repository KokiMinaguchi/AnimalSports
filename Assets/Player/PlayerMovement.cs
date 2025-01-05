using R3;
using R3.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

/// <summary>
/// プレイヤー移動クラス
/// </summary>
[RequireComponent(typeof(PlayerInputProvider))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField, Range(1f, 200f)]
    [Header("ジャンプ力")]
    private float _jumpPower;

    [SerializeField]
    private Vector3 _localGravity;

    //private PlayerInput _playerInput;
    private Vector2 _walk;
    private bool _dash;

    private float _targetRotation = 0.0f;
    public float RotationSmoothTime = 0.12f;
    GameObject _mainCamera;
    [SerializeField]
    private float _walkSpeed;
    [SerializeField]
    private float _dashSpeed;
    private float _speed;
    private float _rotationVelocity;
    private float _verticalVelocity;

    public GameObject _target;

    //bool _isShot = false;// 弾発射トリガー
    [SerializeField]
    private float _bulletSpeed;// 発射スピード
    [SerializeField,Range(0.0f, 5.0f)]
    private float _bulletInterval;// 発射間隔
    private float _bulletTimer = 0.0f;
    [SerializeField]
    private GameObject _bulletPrefab;

    #region Field

    private PlayerInputProvider _inputProvider;
    private Rigidbody _rb;

    public bool _isGround = false;

    #endregion

    float _dragSpeed;
    private void Awake()
    {
        //_playerInput = new PlayerInput();
    }
    // Start is called before the first frame update
    void Start()
    {
        //TryGetComponent(out _playerInput);
        //Debug.Log(_playerInput);
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _inputProvider = GetComponent<PlayerInputProvider>();
        _rb = GetComponent<Rigidbody>();
        _verticalVelocity = 0.0f;

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
                _verticalVelocity = Mathf.Sqrt(_jumpPower * -2f * -_localGravity.y);
                _rb.AddForce(Vector3.up * _jumpPower, ForceMode.Impulse);
                _isGround = false;
            })
            .AddTo(this);

        // 更新処理
        this.FixedUpdateAsObservable()
            .Subscribe(_ =>
            {
                Vector3 inputDirection =
                _mainCamera.transform.forward * _inputProvider.Move.CurrentValue.y +
                _mainCamera.transform.right * _inputProvider.Move.CurrentValue.x;
                
                float targetSpeed = _dash ? _dashSpeed : _walkSpeed;
                if (_inputProvider.Move.CurrentValue == Vector2.zero)
                {
                    targetSpeed = 0.0f;
                    _dragSpeed = 5f;
                    //_rb.velocity = new Vector3(0.5f, 0.5f, 0.5f);
                }
                else
                {
                    _dragSpeed = 0.9f;
                }
                // accelerate or decelerate to target speed
                //if (currentHorizontalSpeed < targetSpeed - speedOffset ||
                //    currentHorizontalSpeed > targetSpeed + speedOffset)
                //{
                //    // creates curved result rather than a linear one giving a more organic speed change
                //    // note T in Lerp is clamped, so we don't need to clamp our speed
                //    _speed = Mathf.Lerp(currentHorizontalSpeed, targetSpeed * inputMagnitude,
                //        Time.deltaTime * SpeedChangeRate);

                //    // round speed to 3 decimal places
                //    _speed = Mathf.Round(_speed * 1000f) / 1000f;
                //}
                //else
                //{
                //    _speed = targetSpeed;
                //}
                _speed = targetSpeed;
                _rb.drag = _dragSpeed;
                if (_inputProvider.Move.CurrentValue != Vector2.zero)
                {
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;// +
                                                                                                      //_mainCamera.transform.eulerAngles.y;
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);

                    // rotate to face input direction relative to camera position
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }
                float test = 1f;    
                if (_isGround == false)
                {
                    test = 0.1f;
                    //_rb.AddForce(_accel, ForceMode.Acceleration);
                    //_moveVelocity = _moveVelocity * _accel.x;
                    //finalSpeed = finalSpeed * _accel.x;
                    //_rb.AddForce(_targetDirection.normalized * factor, ForceMode.Acceleration);
                    //_rb.AddForce(_localGravity, ForceMode.Acceleration);
                    _rb.AddForce(-_localGravity);
                    _verticalVelocity += -_localGravity.y * Time.fixedDeltaTime;
                }

                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
                _rb.AddForce(targetDirection.normalized * (_speed) * test
                    /* +new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime*/);
            })
            .AddTo(this);
    }

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

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
    }

    //public void OnFire(InputAction.CallbackContext context)
    //{
        
    //    Debug.Log(context.phase);
        

    //    switch (context.phase)
    //    {
    //        case InputActionPhase.Started:
    //            _isShot = true;
    //            break;

    //        case InputActionPhase.Canceled:
    //            _isShot = false;
    //            _bulletTimer = 0.0f;
    //            break;

    //        default:
    //            break;
    //    }
    //}

    private void Shot()
    {
        if(_bulletTimer < 0.0f)
        {
            GameObject newbullet = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity); //弾を生成
            Rigidbody bulletRigidbody = newbullet.GetComponent<Rigidbody>();
            bulletRigidbody.AddForce(_target.transform.forward * _bulletSpeed); //キャラクターが向いている方向に弾に力を加える
            Destroy(newbullet, 3); //10秒後に弾を消す

            _bulletTimer = _bulletInterval;
        }
        else
        {
            _bulletTimer -= Time.deltaTime;
        }

        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _isGround = true;
        }
    }
}

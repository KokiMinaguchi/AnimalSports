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
[RequireComponent(typeof(PlayerParameter))]
[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement2D : MonoBehaviour
{
    #region Field

    private bool _dash;

    [SerializeField, Range(50.0f, 100.0f)]
    [Header("移動スピード")]
    private float _walkSpeed;

    [SerializeField]
    private float _dashSpeed;

    private float _speed;
    private float _rotationVelocity;

    private float _targetRotation = 0.0f;
    [SerializeField, Range(0.1f, 5.0f)]
    [Header("回転速度")]
    public float RotationSmoothTime = 0.12f;

    [SerializeField]
    private GameObject _aimTarget;

    [SerializeField, Range(1.0f, 200.0f)]
    [Header("ジャンプ力")]
    private float _jumpPower;

    [SerializeField]
    [Header("空中で自由に移動できなくするための慣性係数")]
    private float _jumpingInertia = 1.0f;

    [SerializeField, Range(0.0f, 1.0f)]
    [Header("減衰係数")]
    private float _attenuation;

    [SerializeField]
    private Vector3 _localGravity;

    private Rigidbody _rb;
    private GameObject _mainCamera;
    // 入力プロバイダ
    private PlayerInputProvider _inputProvider;
    // プレイヤーの各種パラメータ
    private PlayerParameter _parameter;

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        // シーン全体の重力を設定
        Physics.gravity = new Vector3(0, -50, 0);

        _rb = GetComponent<Rigidbody>();
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        // プレイヤー入力
        _inputProvider = GetComponent<PlayerInputProvider>();
        // プレイヤーの各種パラメータ
        _parameter = GetComponent<PlayerParameter>();

        // 移動キーを入力していないとき
        _inputProvider.Move
            .Where(value => value.magnitude <= 0.4f)
            .Subscribe(_ =>
            {
                _speed = 0f;
                Debug.Log("STOP");
            })
            .AddTo(this);

        // 移動キーを入力しているとき
        _inputProvider.Move
            .Where(value => value.magnitude > 0.5f)
            .Subscribe(_ =>
            {
                _speed = _walkSpeed;
                Debug.Log("MOVE");
            })
            .AddTo(this);

        // ジャンプ
        _inputProvider.Jump
            .Where(_ => _parameter.IsGround == true)
            .Subscribe(_ =>
            {
                Debug.Log("JUMP");
                _rb.AddForce(Vector3.up * Mathf.Sqrt(_jumpPower * -2f * -_localGravity.y), ForceMode.Impulse);
                _parameter.IsGround = false;
            })
            .AddTo(this);

        // 更新処理
        this.FixedUpdateAsObservable()
            .Subscribe(_ =>
            {
                // 入力方向計算（カメラの方向を進行方向にする）
                Vector3 inputDirection =
                _mainCamera.transform.forward * _inputProvider.Move.CurrentValue.y +
                _mainCamera.transform.right * _inputProvider.Move.CurrentValue.x;

                float targetSpeed = _dash ? _dashSpeed : _walkSpeed;
                // 移動が入力されていないときは加速度を減衰させる
                if (_inputProvider.Move.CurrentValue == Vector2.zero)
                {
                    _rb.velocity = new Vector3(
                        _rb.velocity.x * _attenuation, _rb.velocity.y, _rb.velocity.z * _attenuation);
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

                // プレイヤーが動いている間、移動方向を向くために回転する
                if (_inputProvider.Move.CurrentValue != Vector2.zero)
                {
                    _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;// +
                                                                                                      //_mainCamera.transform.eulerAngles.y;
                    float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                        RotationSmoothTime);

                    // rotate to face input direction relative to camera position
                    transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
                }

                // ジャンプ中は慣性係数で移動を制限
                _jumpingInertia = 1.0f;
                if (_parameter.IsGround == false)
                {
                    _jumpingInertia = 0.1f;
                }

                // プレイヤー移動
                Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
                _rb.AddForce(targetDirection.normalized * (_speed) * _jumpingInertia);
            })
            .AddTo(this);
    }

    public void OnDash(InputAction.CallbackContext context)
    {
        switch (context.phase)
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
}

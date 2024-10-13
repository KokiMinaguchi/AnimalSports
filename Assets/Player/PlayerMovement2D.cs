using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

[RequireComponent(typeof(PlayerInputProvider))]
[RequireComponent (typeof(Rigidbody))]
public class PlayerMovement2D : MonoBehaviour
{
    private PlayerInputProvider _inputProvider;
    private bool _dash;

    private Rigidbody _rb;
    float _dragSpeed;

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

    [SerializeField, Range(1f, 20f)]
    private float _jumpPower;


    public GameObject _target;

    public bool _isGround;

    [SerializeField]
    private Vector3 _localGravity;

    Vector3 _inputDirection;
    Vector3 _targetDirection;
    Vector3 _moveVelocity;

    private void Awake()
    {
        
    }
    // Start is called before the first frame update
    void Start()
    {
        _mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        _rb = GetComponent<Rigidbody>();
        _inputProvider = GetComponent<PlayerInputProvider>();

        // 移動キーを入力していないとき
        _inputProvider.Move.Where(value => value.magnitude <= 0.5f)
            .Subscribe(_ => 
            { 
                _speed = 0f;
                _dragSpeed = 5f;
                _rb.drag = _dragSpeed;
                
            }).AddTo(this);

        // 移動キーを入力しているとき
        _inputProvider.Move.Where(value => value.magnitude > 0.5f)
            .Subscribe(_ =>
            {
                _speed = _walkSpeed;
                _dragSpeed = 0.9f;
                _rb.drag = _dragSpeed;
              //PlayerRotation();
                

            }).AddTo(this);

        // ジャンプ
        _inputProvider.Jump.Where(_ => _isGround == true).Subscribe(_ =>
        {
            Debug.Log("JUMP");
            _verticalVelocity = _jumpPower;
            _rb.AddForce(Vector3.up * _verticalVelocity, ForceMode.Impulse);
            _isGround = false;
        }).AddTo(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        _inputDirection = 
            _mainCamera.transform.forward * _inputProvider.Move.CurrentValue.y + 
            _mainCamera.transform.right * _inputProvider.Move.CurrentValue.x;

        float targetSpeed = _dash ? _dashSpeed : _walkSpeed;
        
        if (_inputProvider.Move.CurrentValue != Vector2.zero)
        {
            PlayerRotation();
        }

        _targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;

        _moveVelocity = _targetDirection.normalized * (_speed * Time.fixedDeltaTime);
        //  滑る移動
        _rb.AddForce(new Vector3(_moveVelocity.x, _verticalVelocity, _moveVelocity.z));
        // すべらない移動
        //_rb.velocity = new Vector3(moveVelocity.x, _verticalVelocity, moveVelocity.z);

        if (_isGround == false)
        {
            _rb.AddForce(_localGravity, ForceMode.Acceleration);
            //_verticalVelocity += _localGravity.y * Time.fixedDeltaTime;
        }
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

    //public void OnJump(InputAction.CallbackContext context)
    //{
    //    if (context.performed == false || _isGround == false) return;
    //    Debug.Log("Jump");
    //    _verticalVelocity = _jumpPower;
    //    _rb.AddForce(Vector3.up * _verticalVelocity, ForceMode.Impulse);
    //    _isGround = false;
    //}

    private void PlayerRotation()
    {
        _targetRotation = Mathf.Atan2(_inputDirection.x, _inputDirection.z) * Mathf.Rad2Deg;// +
                                                                                          //_mainCamera.transform.eulerAngles.y;
        float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
            RotationSmoothTime);

        // rotate to face input direction relative to camera position
        transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _isGround = true;
        }
    }
}

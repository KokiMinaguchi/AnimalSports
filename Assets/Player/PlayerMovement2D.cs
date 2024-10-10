using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class PlayerMovement2D : MonoBehaviour
{
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

    [SerializeField, Range(1f, 20f)]
    private float _jumpPower;


    public GameObject _target;

    public bool _isGround;

    bool _isShot = false;// íeî≠éÀÉgÉäÉKÅ[
    [SerializeField]
    private float _bulletSpeed;// î≠éÀÉXÉsÅ[Éh
    [SerializeField,Range(0.0f, 5.0f)]
    private float _bulletInterval;// î≠éÀä‘äu
    private float _bulletTimer = 0.0f;
    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private Vector3 _localGravity;

    Rigidbody _rb;
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
        _rb = GetComponent<Rigidbody>();
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        //Vector3 inputDirection = new Vector3(_walk.x, 0.0f, _walk.y);
        Vector3 inputDirection = _mainCamera.transform.forward * _walk.y + _mainCamera.transform.right * _walk.x;
        float targetSpeed = _dash ? _dashSpeed : _walkSpeed;
        
        if (_walk == Vector2.zero)
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
        if (_walk != Vector2.zero)
        {
            _targetRotation = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg;// +
                              //_mainCamera.transform.eulerAngles.y;
            float rotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, _targetRotation, ref _rotationVelocity,
                RotationSmoothTime);

            // rotate to face input direction relative to camera position
            transform.rotation = Quaternion.Euler(0.0f, rotation, 0.0f);
        }


        Vector3 targetDirection = Quaternion.Euler(0.0f, _targetRotation, 0.0f) * Vector3.forward;
        targetDirection.y = 0;
        //Debug.Log("target:"+targetDirection);
        Vector3 moveVelocity = targetDirection.normalized * (_speed * Time.fixedDeltaTime);

        //  ääÇÈà⁄ìÆ
        _rb.AddForce(new Vector3(moveVelocity.x, _verticalVelocity, moveVelocity.z));
        // Ç∑Ç◊ÇÁÇ»Ç¢à⁄ìÆ
        //_rb.velocity = new Vector3(moveVelocity.x, _verticalVelocity, moveVelocity.z);


        //_rb.velocity = targetDirection.normalized * (_speed * Time.fixedDeltaTime) + new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.fixedDeltaTime;
        //_rb.velocity *= 100f;
        //Debug.Log(targetDirection.normalized * (_speed * Time.deltaTime) +
        //                     new Vector3(0.0f, _verticalVelocity, 0.0f) * Time.deltaTime);
        //Debug.Log(_rb.velocity);
        if(_isShot)
        {
            Shot();
        }
        if (_isGround == false)
        {
            _rb.AddForce(_localGravity, ForceMode.Acceleration);
            //_verticalVelocity += _localGravity.y * Time.fixedDeltaTime;
        }

    }

    public void OnWalk(InputAction.CallbackContext context)
    {
        _walk = context.ReadValue<Vector2>();
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

    public void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed == false || _isGround == false) return;
        Debug.Log("Jump");
        _verticalVelocity = _jumpPower;
        _rb.AddForce(Vector3.up * _verticalVelocity, ForceMode.Impulse);
        _isGround = false;
    }

    public void OnAttack(InputAction.CallbackContext context)
    {
        Debug.Log(context.phase);
    }

    public void OnFire(InputAction.CallbackContext context)
    {
        
        Debug.Log(context.phase);
        

        switch (context.phase)
        {
            case InputActionPhase.Started:
                _isShot = true;
                break;

            case InputActionPhase.Canceled:
                _isShot = false;
                _bulletTimer = 0.0f;
                break;

            default:
                break;
        }
    }

    private void Shot()
    {
        if(_bulletTimer < 0.0f)
        {
            GameObject newbullet = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity); //íeÇê∂ê¨
            newbullet.transform.position += this.transform.forward * 2f;
            Rigidbody bulletRigidbody = newbullet.GetComponent<Rigidbody>();
            newbullet.gameObject.tag = "Bullet";
            bulletRigidbody.AddForce(_target.transform.forward * _bulletSpeed); //ÉLÉÉÉâÉNÉ^Å[Ç™å¸Ç¢ÇƒÇ¢ÇÈï˚å¸Ç…íeÇ…óÕÇâ¡Ç¶ÇÈ
            Destroy(newbullet, 3); //10ïbå„Ç…íeÇè¡Ç∑

            _bulletTimer = _bulletInterval;
        }
        else
        {
            _bulletTimer -= Time.deltaTime;
        }

        
    }

    //private void OnEnable()
    //{
    //    _playerInput.Enable();
    //}

    //private void OnDisable()
    //{
    //    _playerInput.Disable();
    //}

    //private void OnDestroy()
    //{
    //    _playerInput.Dispose();
    //}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Ground")
        {
            _isGround = true;
        }
    }
}

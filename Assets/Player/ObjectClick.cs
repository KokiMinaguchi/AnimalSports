using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputProvider))]
public class ObjectClick : MonoBehaviour
{
    private PlayerInputProvider _inputProvider;
    private RaycastHit _hit;
    private GameObject clickedGameObject;
    private Transform _transform;
    private Rigidbody _rb;

    public GameObject _canvas;

    [SerializeField, Range(1.0f, 50.0f)]
    private float _jumpPower = 20.0f;
    bool _isOpenMenu = false;

    // Start is called before the first frame update
    void Start()
    {
        // �v���C���[����
        _inputProvider = GetComponent<PlayerInputProvider>();
        
        _inputProvider.OpenMenu
            .Where(value => value == true)
            .Subscribe(_ =>
        {
            _isOpenMenu ^= true;
            if (_isOpenMenu)
            {
                _inputProvider.InputAction.Game.Disable();
                _inputProvider.InputAction.UI.Enable();
                _canvas.SetActive(true);
            }
            else
            {
                _inputProvider.InputAction.Game.Enable();
                _inputProvider.InputAction.UI.Disable();
                _canvas.SetActive(false);
            }
        })
        .AddTo(this);

        

        _transform = GetComponent<Transform>();
        _rb = GetComponent<Rigidbody>();
        //_eventTrigger.OnPointerClick = OnPointerClick;
        _inputProvider.ClickAimTarget
            .Where(value => value == true)
            .Subscribe(_ =>
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                //RaycastHit hit = new RaycastHit();
                if (Physics.Raycast(ray, out _hit))
                {
                    clickedGameObject = _hit.collider.gameObject;
                    Debug.Log(clickedGameObject.name);//�Q�[���I�u�W�F�N�g�̖��O���o��
                    //Destroy(clickedGameObject);//�Q�[���I�u�W�F�N�g��j��
                    Vector3 dir = clickedGameObject.transform.position - _transform.position;
                    dir = new Vector3(dir.x, dir.y, 0);
                    // ���񓯂��͂Ŕ�Ԃ��߂ɂO�ŏ���������
                    _rb.velocity = Vector3.zero;
                    _rb.AddForce(dir.normalized * _jumpPower, ForceMode.VelocityChange);
                }
            })
            .AddTo(this);
    }
}

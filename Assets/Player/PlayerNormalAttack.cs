using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[�ʏ�U���N���X
/// </summary>
[RequireComponent(typeof(PlayerInputProvider))]
public class PlayerNormalAttack : MonoBehaviour
{
    #region SerializeField

    [SerializeField]
    [Header("�e�̃X�s�[�h")]
    private float _bulletSpeed;

    [SerializeField, Range(0.0f, 5.0f)]
    [Header("���ˊԊu")]
    private float _bulletInterval;

    [SerializeField]
    [Header("�J����������ʒu")]
    private GameObject _aimTarget;

    [SerializeField]
    [Header("�e�̃v���n�u")]
    private GameObject _bulletPrefab;

    #endregion

    #region Field

    private PlayerInputProvider _inputProvider;

    private Transform _transform;

    private float _bulletTimer = 0.0f;

    #endregion

    #region Unity

    // Start is called before the first frame update
    void Start()
    {
        _inputProvider = GetComponent<PlayerInputProvider>();
        _transform = GetComponent<Transform>();

        // �U���{�^�����������Ƃ��^�C�}�[���Z�b�g
        _inputProvider.NormalAttack
            .Where(value => value)
            .Subscribe(_ => _bulletTimer = 0.0f)
            .AddTo(this);

        // �U���{�^���������Ă���ԁA���t���[�����s
        this.FixedUpdateAsObservable()
            .Where(_ => _inputProvider.NormalAttack.CurrentValue)
            .Subscribe(_ =>
            {
                _bulletTimer -= Time.fixedDeltaTime;
                Shot();
            })
            .AddTo(this);
    }

    #endregion

    #region Method

    private void Shot()
    {
        if (_bulletTimer > 0.0f) return;

        // TODO�@ObjectPool
        GameObject newbullet = Instantiate(_bulletPrefab, _transform.position, Quaternion.identity); //�e�𐶐�
        newbullet.transform.position += _transform.forward * 2f;
        Rigidbody bulletRigidbody = newbullet.GetComponent<Rigidbody>();
        newbullet.gameObject.tag = "Bullet";
        bulletRigidbody.AddForce(_aimTarget.transform.forward * _bulletSpeed); //�L�����N�^�[�������Ă�������ɒe�ɗ͂�������
        Destroy(newbullet, 3); //3�b��ɒe������
        _bulletTimer = _bulletInterval;
    }

    #endregion
}

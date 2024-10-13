using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerInputProvider))]
public class PlayerNormalAttack : MonoBehaviour
{
    private PlayerInputProvider _inputProvider;

    public GameObject _target;

    public bool _isGround;


    [SerializeField]
    [Header("弾のスピード")]
    private float _bulletSpeed;

    [SerializeField, Range(0.0f, 5.0f)]
    [Header("発射間隔")]
    private float _bulletInterval;

    private float _bulletTimer = 0.0f;

    [SerializeField]
    private GameObject _bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        _inputProvider = GetComponent<PlayerInputProvider>();

        // 攻撃ボタンを押したときタイマーリセット
        _inputProvider.NormalAttack
            .Where(value => value)
            .Subscribe(_ => _bulletTimer = 0.0f)
            .AddTo(this);

        // 攻撃ボタンを押している間、毎フレーム実行
        this.FixedUpdateAsObservable()
            .Where(_ => _inputProvider.NormalAttack.CurrentValue)
            .Subscribe(_ =>
            {
                _bulletTimer -= Time.fixedDeltaTime;
                Shot();
            })
            .AddTo(this);
    }

    private void Shot()
    {
        if (_bulletTimer > 0.0f) return;

        // TODO　ObjectPool
        GameObject newbullet = Instantiate(_bulletPrefab, this.transform.position, Quaternion.identity); //弾を生成
        newbullet.transform.position += this.transform.forward * 2f;
        Rigidbody bulletRigidbody = newbullet.GetComponent<Rigidbody>();
        newbullet.gameObject.tag = "Bullet";
        bulletRigidbody.AddForce(_target.transform.forward * _bulletSpeed); //キャラクターが向いている方向に弾に力を加える
        Destroy(newbullet, 3); //3秒後に弾を消す
        _bulletTimer = _bulletInterval;
    }
}

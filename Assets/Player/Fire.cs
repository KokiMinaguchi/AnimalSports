using R3;
using R3.Triggers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    private PlayerInputProvider _inputProvider;

    [SerializeField]
    private GameObject _bulletPrefab;

    [SerializeField]
    private Transform _fireTarget;

    [SerializeField]
    private float _bulletSpeed;

    [SerializeField]
    float bulletInterval;
    float bulletTimer = 0.0f;

    // Start is called before the first frame update
    void Start()
    {
        _inputProvider = GetComponent<PlayerInputProvider>();

        this.FixedUpdateAsObservable().Subscribe(_ =>
        {
            if (_inputProvider.Fire.CurrentValue == true && bulletTimer <= 0.0f)
            {
                GameObject newBullet = Instantiate(_bulletPrefab, _fireTarget.position, Quaternion.identity);
                newBullet.GetComponent<Rigidbody>().AddForce(transform.forward * _bulletSpeed);
                Destroy(newBullet, 10);
                bulletTimer = bulletInterval;
            }
            bulletTimer -= Time.fixedDeltaTime;
        })
        .AddTo(this);
    }
}

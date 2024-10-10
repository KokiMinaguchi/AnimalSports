using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;

public class HitPointPresenter : MonoBehaviour
{
    [Header("HPをつける対象")]
    [SerializeField]
    private GameObject _character;

    private HitPointModel _hpModel;
    private HitPointView _hpView;

    [SerializeField]
    string _hitTarget;

    void Start()
    {
        _hpModel = this.GetComponent<HitPointModel>();
        _hpView = this.GetComponent<HitPointView>();
        // 攻撃が当たった時の処理
        _character.OnTriggerEnterAsObservable()
            .Where(other => other.gameObject.CompareTag(_hitTarget))
            .Subscribe(_ => _hpModel.AddDamage(10));



        // デバッグ
        _character.OnTriggerEnterAsObservable().Subscribe(_ => Debug.Log(_.gameObject.name));
        _hpModel.HP.Where(hp => hp <= 0).Subscribe(_ => Debug.Log("HP0!"));

        // HPModel内のHPが減ったことをViewへ知らせる
        _hpModel.HP
            .Subscribe(hp => _hpView.SetGuage(_hpModel._maxHP, hp)).AddTo(this);
    }
}

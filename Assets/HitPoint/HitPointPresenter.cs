using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using R3.Triggers;

public class HitPointPresenter : MonoBehaviour
{
    [Header("HP������Ώ�")]
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
        // �U���������������̏���
        _character.OnTriggerEnterAsObservable()
            .Where(other => other.gameObject.CompareTag(_hitTarget))
            .Subscribe(_ => _hpModel.AddDamage(10));



        // �f�o�b�O
        _character.OnTriggerEnterAsObservable().Subscribe(_ => Debug.Log(_.gameObject.name));
        _hpModel.HP.Where(hp => hp <= 0).Subscribe(_ => Debug.Log("HP0!"));

        // HPModel����HP�����������Ƃ�View�֒m�点��
        _hpModel.HP
            .Subscribe(hp => _hpView.SetGuage(_hpModel._maxHP, hp)).AddTo(this);
    }
}

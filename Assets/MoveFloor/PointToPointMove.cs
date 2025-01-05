using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using R3;
using R3.Triggers;
using DG.Tweening;
using System;

public class PointToPointMove : MonoBehaviour
{
    #region Property

    public ReadOnlyReactiveProperty<bool> IsMoving => _isMoving;

    #endregion
    #region SerializeField

    [SerializeField]
    private Transform[] _pointMoveList;

    [SerializeField]
    private float _speed;

    [SerializeField]
    private float _stopTime;

    [SerializeField]
    Switch _switch;

    #endregion

    #region Field

    private Transform _transform;
    private Vector3[] _movePoint;

    public ReactiveProperty<bool> _isMoving = new(true);

    #endregion

    // Start is called before the first frame update
    void Start()
    {
        _transform = GetComponent<Transform>();
        
        _movePoint = new Vector3[_pointMoveList.Length];
        for(int i = 0; i < _pointMoveList.Length; ++i)
        {
            _movePoint[i] = _pointMoveList[i].position;
        }

        //_isMoving
        //    .Where(value => value == true)
        //    .Subscribe(_ =>
        //    {
        //        _isMoving.Value = false;
        //        _transform.DOPath(_movePoint, 3f).onComplete = SetMove;
        //    })
        //    .AddTo(this);
    }

    private void SetMove()
    {
        Debug.Log("Complete");
        Vector3 temp;
        temp = _movePoint[0];
        _movePoint[0] = _movePoint[_movePoint.Length - 1];
        _movePoint[_movePoint.Length - 1] = temp;
        _isMoving.Value = true;
    }

    private void OnDestroy()
    {
        _isMoving.Dispose();
    }

    public void Move()
    {
        _transform.DOMove(_movePoint[0], 3f).onComplete = MoveCompleted;
        //_transform.DOPath(_movePoint, _speed).onComplete = MoveCompleted;
    }

    private void MoveCompleted()
    {
        _switch.SetOnOFF(true);
        SetNextMovePoint();
    }

    private void SetNextMovePoint()
    {
        Vector3 temp;
        temp = _movePoint[0];
        _movePoint[0] = _movePoint[_movePoint.Length - 1];
        _movePoint[_movePoint.Length - 1] = temp;
    }
}

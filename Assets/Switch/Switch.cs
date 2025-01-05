using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool _isOnOFF;

    [SerializeField]
    public PointToPointMove _linkedObjectAction;
    // Start is called before the first frame update
    void Start()
    {
        _isOnOFF = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetOnOFF(bool value) { _isOnOFF = value; }



    private void OnTriggerEnter(Collider other)
    {
        if (_isOnOFF == true)
        {
            _linkedObjectAction.Move();
            _isOnOFF = false;
        }
    }
}

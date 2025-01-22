using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCheckJump : MonoBehaviour
{
    private PlayerParameter _playerParameter;
    private void Start()
    {
        _playerParameter = this.GetComponentInParent<PlayerParameter>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _playerParameter.IsGround = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        _playerParameter.IsGround = false;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Ground")
        {
            _playerParameter.IsGround = true;
        }
    }
}

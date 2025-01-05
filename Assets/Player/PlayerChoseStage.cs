using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerChoseStage : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "StageFigure")
        {
            Debug.Log("EnterStage");
            Debug.Log(other.gameObject.name);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "StageFigure")
        {
            Debug.Log("ExitStage");
            Debug.Log(other.gameObject.name);
        }
    }
}

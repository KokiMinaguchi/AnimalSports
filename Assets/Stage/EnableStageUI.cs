using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableStageUI : MonoBehaviour
{
    [SerializeField]
    private Canvas _stageCanvas;

    private void OnCollisionEnter(Collision collision)
    {
        if(CompareTag("StageFigure"))
        {
            _stageCanvas.enabled = true;
            Debug.Log("EnterStage");
            Debug.Log(collision.gameObject.name);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if(CompareTag("StageFigure"))
        {
            _stageCanvas.enabled = false;
            Debug.Log("ExitStage");
            Debug.Log(collision.gameObject.name);
        }
    }
    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.gameObject.tag == "StageFigure")
    //    {
    //        _stageCanvas.enabled = true;
    //        Debug.Log("EnterStage");
    //        Debug.Log(other.gameObject.name);
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.gameObject.tag == "StageFigure")
    //    {
    //        _stageCanvas.enabled = false;
    //        Debug.Log("ExitStage");
    //        Debug.Log(other.gameObject.name);
    //    }
    //}
}

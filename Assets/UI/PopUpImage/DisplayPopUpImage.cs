using SleepingAnimals.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class DisplayPopUpImage : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        [Header("表示切り替えされる対象のオブジェクト")]
        private Tag _hitTag;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag(_hitTag.Name))
            {
                _canvasGroup.alpha = 1.0f;
            }
        }
        private void OnCollisionExit(Collision collision)
        {
            if (collision.gameObject.CompareTag(_hitTag.Name))
            {
                _canvasGroup.alpha = 0.0f;
            }
        }
    }
}

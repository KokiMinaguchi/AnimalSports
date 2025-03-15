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
        [Header("�\���؂�ւ������Ώۂ̃I�u�W�F�N�g")]
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

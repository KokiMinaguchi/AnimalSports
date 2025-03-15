using SleepingAnimals.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// �I�u�W�F�N�g�̏��UI��\��
    /// </summary>
    public class PopUpImage : MonoBehaviour
    {
        [SerializeField]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        GameObject _player;

        private RectTransform _transform;

        [SerializeField]
        private Vector3 offset;

        [SerializeField]
        [Header("�\���؂�ւ������Ώۂ̃I�u�W�F�N�g")]
        private Tag _hitTag;

        private void Start()
        {
            //_canvasGroup = _canvasGroup.GetComponent<CanvasGroup>();
            _transform = GetComponent<RectTransform>();
            _canvasGroup.alpha = 0.0f;
        }

        void Update()
        {
            _transform.position
                = RectTransformUtility.WorldToScreenPoint(Camera.main, _player.transform.position + offset);
        }

        /// <summary>
        /// �Ώۂ̃I�u�W�F�N�g�ɓ���������UI�̕\���؂�ւ�
        /// </summary>
        /// <param name="collision"></param>
        //private void OnCollisionEnter(Collision collision)
        //{
        //    if (collision.gameObject.CompareTag(_hitTag.Name))
        //    {
        //        _canvasGroup.alpha = 1.0f;
        //    }
        //}

        //private void OnCollisionExit(Collision collision)
        //{
        //    if (collision.gameObject.CompareTag(_hitTag.Name))
        //    {
        //        _canvasGroup.alpha = 0.0f;
        //    }
        //}
    }
}

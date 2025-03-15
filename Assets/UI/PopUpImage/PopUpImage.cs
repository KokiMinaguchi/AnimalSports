using SleepingAnimals.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// オブジェクトの上にUIを表示
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
        [Header("表示切り替えされる対象のオブジェクト")]
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
        /// 対象のオブジェクトに当たったらUIの表示切り替え
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

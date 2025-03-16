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
        [Header("ゲームシーンカメラ")]
        private Camera _mainCamera;

        [SerializeField]
        [Header("操作UIのキャンバスグループ")]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        [Header("プレイヤー座標")]
        private GameObject _player;

        [SerializeField]
        [Header("操作UIをプレイヤー基準でどれだけ移動させるか")]
        private Vector3 offset;

        private RectTransform _transform;

        private void Start()
        {
            _transform = GetComponent<RectTransform>();
            _canvasGroup.alpha = 0.0f;
        }

        void Update()
        {
            // 操作UIをプレイヤーの横に移動させる
            _transform.position
                = RectTransformUtility.WorldToScreenPoint(_mainCamera, _player.transform.position + offset);
        }
    }
}

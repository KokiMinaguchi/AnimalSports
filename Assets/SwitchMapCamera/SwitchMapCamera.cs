using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Cinemachine;

namespace SleepingAnimals
{
    /// <summary>
    /// プレイヤーカメラとマップカメラをスイッチする
    /// </summary>
    [RequireComponent(typeof(PlayerInputProvider))]
    public class SwitchMapCamera : MonoBehaviour
    {
        [SerializeField]
        [Header("プレイヤーカメラ")]
        private CinemachineVirtualCameraBase _playerCamera;

        [SerializeField]
        [Header("マップカメラ")]
        private CinemachineVirtualCameraBase _mapCamera;

        private PlayerInputProvider _inputProvider;

        // Start is called before the first frame update
        void Start()
        {
            _inputProvider = GetComponent<PlayerInputProvider>();

            // Mボタンが押されたらステージ全体を表示
            _inputProvider.OpenMap
                .Where(value => value == true)
                .Subscribe(_ =>
                {
                    // VirtualCamera優先度の入れ替え
                    int tmp;
                    tmp = _playerCamera.Priority;
                    _playerCamera.Priority = _mapCamera.Priority;
                    _mapCamera.Priority = tmp;
                })
                .AddTo(this);
        }
    }
}

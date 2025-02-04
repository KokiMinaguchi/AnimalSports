using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using Cinemachine;

namespace SleepingAnimals
{
    /// <summary>
    /// �v���C���[�J�����ƃ}�b�v�J�������X�C�b�`����
    /// </summary>
    [RequireComponent(typeof(PlayerInputProvider))]
    public class SwitchMapCamera : MonoBehaviour
    {
        [SerializeField]
        [Header("�v���C���[�J����")]
        private CinemachineVirtualCameraBase _playerCamera;

        [SerializeField]
        [Header("�}�b�v�J����")]
        private CinemachineVirtualCameraBase _mapCamera;

        private PlayerInputProvider _inputProvider;

        // Start is called before the first frame update
        void Start()
        {
            _inputProvider = GetComponent<PlayerInputProvider>();

            // M�{�^���������ꂽ��X�e�[�W�S�̂�\��
            _inputProvider.OpenMap
                .Where(value => value == true)
                .Subscribe(_ =>
                {
                    // VirtualCamera�D��x�̓���ւ�
                    int tmp;
                    tmp = _playerCamera.Priority;
                    _playerCamera.Priority = _mapCamera.Priority;
                    _mapCamera.Priority = tmp;
                })
                .AddTo(this);
        }
    }
}

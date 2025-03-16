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
        [Header("�Q�[���V�[���J����")]
        private Camera _mainCamera;

        [SerializeField]
        [Header("����UI�̃L�����o�X�O���[�v")]
        private CanvasGroup _canvasGroup;

        [SerializeField]
        [Header("�v���C���[���W")]
        private GameObject _player;

        [SerializeField]
        [Header("����UI���v���C���[��łǂꂾ���ړ������邩")]
        private Vector3 offset;

        private RectTransform _transform;

        private void Start()
        {
            _transform = GetComponent<RectTransform>();
            _canvasGroup.alpha = 0.0f;
        }

        void Update()
        {
            // ����UI���v���C���[�̉��Ɉړ�������
            _transform.position
                = RectTransformUtility.WorldToScreenPoint(_mainCamera, _player.transform.position + offset);
        }
    }
}

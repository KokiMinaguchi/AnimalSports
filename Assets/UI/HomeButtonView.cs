using CommonViewParts;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SleepingAnimals
{
    /// <summary>
    /// �z�[���{�^���̏�������
    /// </summary>
    [RequireComponent(typeof(CustomButton))]
    public class HomeButtonView : MonoBehaviour
    {
        private CustomButton _button;

        [SerializeField]
        [Header("�X�e�[�W�I���L�����o�X")]
        private CanvasGroup _stageSelectCanvas;

        [SerializeField]
        [Header("�^�C�g���L�����o�X")]
        private CanvasGroup _titleCanvas;

        [SerializeField]
        [Range(0.1f, 2.0f)]
        [Header("�J�ڎ���")]
        private float _fadeTime;

        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<CustomButton>();

            _button.OnButtonClicked.AsObservable().Subscribe(button =>
            {
                // �^�C�g����ʂɖ߂�A������ɃL�����o�X�O���[�v�̐ݒ��ύX����
                _stageSelectCanvas.interactable = false;
                _stageSelectCanvas.blocksRaycasts = false;
                _stageSelectCanvas.DOFade(0.0f, _fadeTime);
                _titleCanvas.DOFade(1.0f, _fadeTime).OnComplete(ValidUI);
            })
            .AddTo(this);
        }

        /// <summary>
        /// �L�����o�X�O���[�v�̐ݒ��L���ɂ���
        /// </summary>
        private void ValidUI()
        {
            // �{�^����L���ɂ���
            _titleCanvas.interactable = true;
            // �^�C�g���ƃX�e�[�W�Z���N�g�𓧖��x�œ���ւ��Ă���e��
            // �Ń{�^�����d�Ȃ��Ĕ������Ȃ���Ԃ��������
            _titleCanvas.blocksRaycasts = true;
        }
    }
}

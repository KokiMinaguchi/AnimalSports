using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using DG.Tweening;
using UnityEngine.UI;

namespace SleepingAnimals
{
    /// <summary>
    /// �Q�[���X�^�[�g�{�^����������
    /// </summary>
    public class GameStartButtonView : BaseButton
    {
        private RectTransform _rectTransform;

        [SerializeField]
        [Header("�^�C�g����ʃL�����o�X")]
        private CanvasGroup _titleCanvas;

        [SerializeField]
        [Header("�X�e�[�W�I����ʃL�����o�X")]
        private CanvasGroup _stageSelectCanvas;

        // �Q�[���X�^�[�gUI�̂ݓ_�ł����邽�߂ɃL�����o�X�𕪂���
        [SerializeField]
        [Header("�Q�[���X�^�[�gUI�L�����o�X")]
        private CanvasGroup _gameStartCanvas;

        [SerializeField]
        [Range(0.1f, 2.0f)]
        [Header("�J�ڎ���")]
        private float _fadeTime;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _rectTransform = GetComponent<RectTransform>();

            // �_�ŏ���
            _gameStartCanvas.DOFade(0f, 2f).SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo);

            _button.OnButtonPressed
                .AsObservable()
                .Subscribe(_ =>
            {
                // �������Ƃ��̊g�k����
                _rectTransform.DOScale(0.90f, 0.24f).SetEase(Ease.OutCubic);
            })
            .AddTo(this);

            _button.OnButtonReleased
                .AsObservable()
                .Subscribe(_ =>
                {
                    // �������Ƃ��̊g�k����
                    _rectTransform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonClicked
                .AsObservable()
                .Subscribe(_ =>
            {
                // �X�e�[�W�I����ʂɐi�݁A������ɃL�����o�X�O���[�v�̐ݒ��ύX����
                _stageSelectCanvas.DOFade(1.0f, _fadeTime).OnComplete(ValidUI);

                _titleCanvas.interactable = false;
                _titleCanvas.blocksRaycasts = false;
                _titleCanvas.DOFade(0.0f, _fadeTime);
            })
            .AddTo(this);
        }

        /// <summary>
        /// �L�����o�X�O���[�v�̐ݒ��L���ɂ���
        /// </summary>
        private void ValidUI()
        {
            // �{�^����L���ɂ���
            _stageSelectCanvas.interactable = true;
            // �^�C�g���ƃX�e�[�W�Z���N�g�𓧖��x�œ���ւ��Ă���e��
            // �Ń{�^�����d�Ȃ��Ĕ������Ȃ���Ԃ��������
            _stageSelectCanvas.blocksRaycasts = true;
        }
    }
}

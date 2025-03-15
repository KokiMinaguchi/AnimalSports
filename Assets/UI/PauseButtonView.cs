using DG.Tweening;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class PauseButtonView : BaseButton
    {
        private RectTransform _rectTransform;

        [SerializeField]
        CanvasGroup _pauseCanvas;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _button.OnButtonPressed
                .AsObservable()
                .Subscribe(_ =>
                {
                    // �������Ƃ��̊g�k����
                    _rectTransform.DOScale(1.4f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonReleased
                .AsObservable()
                .Subscribe(_ =>
                {
                    // �������Ƃ��̊g�k����
                    _rectTransform.DOScale(1.5f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonClicked
                .AsObservable()
                .Subscribe(_ =>
                {
                    // �|�[�Y��ʕ\��
                    _pauseCanvas.alpha = 1.0f;
                    _pauseCanvas.interactable = true;
                    _pauseCanvas.blocksRaycasts = true;
                })
                .AddTo(this);
        }
    }
}

using DG.Tweening;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class ContinueButtonView : BaseButton
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
                    // 押したときの拡縮処理
                    _rectTransform.DOScale(0.9f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonReleased
                .AsObservable()
                .Subscribe(_ =>
                {
                    // 離したときの拡縮処理
                    _rectTransform.DOScale(1.0f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonClicked
                .AsObservable()
                .Subscribe(_ =>
                {
                    // ポーズ画面非表示
                    _pauseCanvas.alpha = 0.0f;
                    _pauseCanvas.interactable = false;
                    _pauseCanvas.blocksRaycasts = false;
                })
                .AddTo(this);
        }
    }
}

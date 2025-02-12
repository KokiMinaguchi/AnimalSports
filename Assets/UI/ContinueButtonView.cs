using CommonViewParts;
using DG.Tweening;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    [RequireComponent(typeof(CustomButton))]
    public class ContinueButtonView : MonoBehaviour
    {
        private CustomButton _button;
        private RectTransform _rectTransform;

        [SerializeField]
        CanvasGroup _pauseCanvas;

        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<CustomButton>();

            _button.OnButtonPressed
                .AsObservable()
                .Subscribe(_ =>
                {
                    // âüÇµÇΩÇ∆Ç´ÇÃägèkèàóù
                    _rectTransform.DOScale(0.9f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonReleased
                .AsObservable()
                .Subscribe(_ =>
                {
                    // ó£ÇµÇΩÇ∆Ç´ÇÃägèkèàóù
                    _rectTransform.DOScale(1.0f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonClicked
                .AsObservable()
                .Subscribe(_ =>
                {
                    // É|Å[ÉYâÊñ îÒï\é¶
                    _pauseCanvas.alpha = 0.0f;
                    _pauseCanvas.interactable = false;
                    _pauseCanvas.blocksRaycasts = false;
                })
                .AddTo(this);
        }
    }
}

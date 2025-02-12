using CommonViewParts;
using DG.Tweening;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    [RequireComponent(typeof(CustomButton))]
    public class PauseButtonView : MonoBehaviour
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
                    _rectTransform.DOScale(1.4f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonReleased
                .AsObservable()
                .Subscribe(_ =>
                {
                    // ó£ÇµÇΩÇ∆Ç´ÇÃägèkèàóù
                    _rectTransform.DOScale(1.5f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonClicked
                .AsObservable()
                .Subscribe(_ =>
                {
                    // É|Å[ÉYâÊñ ï\é¶
                    _pauseCanvas.alpha = 1.0f;
                    _pauseCanvas.interactable = true;
                    _pauseCanvas.blocksRaycasts = true;
                })
                .AddTo(this);
        }
    }
}

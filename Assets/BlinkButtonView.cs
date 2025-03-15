using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using DG.Tweening;

namespace SleepingAnimals
{
    [RequireComponent(typeof(CustomButton))]
    public class BlinkButtonView : BaseButton
    {
        [SerializeField]
        float _duration;

        [SerializeField]
        float _tweenScaleValue;

        private RectTransform _transform;
        private float _scale;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _transform = GetComponent<RectTransform>();
            _scale = _transform.localScale.x;

            _button.OnButtonPressed.AsObservable().
                Subscribe(_ =>
                {
                    _transform.DOScale(_tweenScaleValue, _duration).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonReleased.AsObservable()
                .Subscribe(_ =>
                {
                    transform.DOScale(_scale, _duration).SetEase(Ease.OutCubic);
                })
                .AddTo(this);
        }
    }
}

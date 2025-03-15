using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SleepingAnimals
{
    public class StageButtonView : BaseButton
    {
        private RectTransform _rectTransform;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _rectTransform = GetComponent<RectTransform>();

            _button.OnButtonPressed
                .AsObservable()
                .Subscribe(_ =>
                {
                    // âüÇµÇΩÇ∆Ç´ÇÃägèkèàóù
                    _rectTransform.DOScale(1.7f, 0.24f).SetEase(Ease.OutCubic);
                })
            .AddTo(this);

            _button.OnButtonReleased
                .AsObservable()
                .Subscribe(_ =>
                {
                    // ó£ÇµÇΩÇ∆Ç´ÇÃägèkèàóù
                    _rectTransform.DOScale(1.8f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonClicked
                .AsObservable()
                .Subscribe(_ =>
                {
                    // ÉVÅ[ÉìëJà⁄
                    //SceneManagement.LoadScene(_sceneName);
                })
                .AddTo(this);
        }
    }
}

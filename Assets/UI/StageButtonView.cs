using CommonViewParts;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace SleepingAnimals
{
    [RequireComponent(typeof(CustomButton))]
    public class StageButtonView : MonoBehaviour
    {
        private CustomButton _button;
        private RectTransform _rectTransform;

        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<CustomButton>();
            _rectTransform = GetComponent<RectTransform>();

            _button.OnButtonPressed
                .AsObservable()
                .Subscribe(_ =>
                {
                    // 押したときの拡縮処理
                    _rectTransform.DOScale(1.7f, 0.24f).SetEase(Ease.OutCubic);
                })
            .AddTo(this);

            _button.OnButtonReleased
                .AsObservable()
                .Subscribe(_ =>
                {
                    // 離したときの拡縮処理
                    _rectTransform.DOScale(1.8f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonClicked
                .AsObservable()
                .Subscribe(_ =>
                {
                    // シーン遷移
                    //SceneManagement.LoadScene(_sceneName);
                })
                .AddTo(this);
        }
    }
}

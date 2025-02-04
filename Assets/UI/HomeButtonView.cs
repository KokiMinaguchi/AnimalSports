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
    /// ホームボタンの処理実装
    /// </summary>
    [RequireComponent(typeof(CustomButton))]
    public class HomeButtonView : MonoBehaviour
    {
        private CustomButton _button;

        [SerializeField]
        [Header("ステージ選択キャンバス")]
        private GameObject _stageSelectCanvas;

        [SerializeField]
        [Header("タイトルキャンバス")]
        private GameObject _titleCanvas;

        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<CustomButton>();

            _button.OnButtonClicked.AsObservable().Subscribe(button =>
            {
                // タイトル画面に戻る
                _stageSelectCanvas.gameObject.SetActive(false);
                _titleCanvas.gameObject.SetActive(true);
            })
            .AddTo(this);
        }
    }
}

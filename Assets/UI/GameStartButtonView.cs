using CommonViewParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using DG.Tweening;

namespace SleepingAnimals
{
    /// <summary>
    /// ゲームスタートボタン処理実装
    /// </summary>
    [RequireComponent(typeof(CustomButton))]
    public class GameStartButtonView : MonoBehaviour
    {
        private CustomButton _button;

        [SerializeField]
        [Header("タイトル画面キャンバス")]
        private GameObject _titleCanvas;

        [SerializeField]
        [Header("ステージ選択画面キャンバス")]
        private GameObject _stageSelectCanvas;

        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<CustomButton>();

            _button.OnButtonClicked.AsObservable().Subscribe(button =>
            {
                // ステージ選択画面に進む
                _titleCanvas.SetActive(false);
                _stageSelectCanvas.SetActive(true);
            })
            .AddTo(this);
        }
    }
}

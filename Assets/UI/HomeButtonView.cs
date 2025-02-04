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
        private CanvasGroup _stageSelectCanvas;

        [SerializeField]
        [Header("タイトルキャンバス")]
        private CanvasGroup _titleCanvas;

        [SerializeField]
        [Range(0.1f, 2.0f)]
        [Header("遷移時間")]
        private float _fadeTime;

        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<CustomButton>();

            _button.OnButtonClicked.AsObservable().Subscribe(button =>
            {
                // タイトル画面に戻り、完了後にキャンバスグループの設定を変更する
                _stageSelectCanvas.interactable = false;
                _stageSelectCanvas.blocksRaycasts = false;
                _stageSelectCanvas.DOFade(0.0f, _fadeTime);
                _titleCanvas.DOFade(1.0f, _fadeTime).OnComplete(ValidUI);
            })
            .AddTo(this);
        }

        /// <summary>
        /// キャンバスグループの設定を有効にする
        /// </summary>
        private void ValidUI()
        {
            // ボタンを有効にする
            _titleCanvas.interactable = true;
            // タイトルとステージセレクトを透明度で入れ替えている影響
            // でボタンが重なって反応しない状態を回避する
            _titleCanvas.blocksRaycasts = true;
        }
    }
}

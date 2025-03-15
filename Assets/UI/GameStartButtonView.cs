using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using DG.Tweening;
using UnityEngine.UI;

namespace SleepingAnimals
{
    /// <summary>
    /// ゲームスタートボタン処理実装
    /// </summary>
    public class GameStartButtonView : BaseButton
    {
        private RectTransform _rectTransform;

        [SerializeField]
        [Header("タイトル画面キャンバス")]
        private CanvasGroup _titleCanvas;

        [SerializeField]
        [Header("ステージ選択画面キャンバス")]
        private CanvasGroup _stageSelectCanvas;

        // ゲームスタートUIのみ点滅させるためにキャンバスを分ける
        [SerializeField]
        [Header("ゲームスタートUIキャンバス")]
        private CanvasGroup _gameStartCanvas;

        [SerializeField]
        [Range(0.1f, 2.0f)]
        [Header("遷移時間")]
        private float _fadeTime;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _rectTransform = GetComponent<RectTransform>();

            // 点滅処理
            _gameStartCanvas.DOFade(0f, 2f).SetEase(Ease.OutBounce).SetLoops(-1, LoopType.Yoyo);

            _button.OnButtonPressed
                .AsObservable()
                .Subscribe(_ =>
            {
                // 押したときの拡縮処理
                _rectTransform.DOScale(0.90f, 0.24f).SetEase(Ease.OutCubic);
            })
            .AddTo(this);

            _button.OnButtonReleased
                .AsObservable()
                .Subscribe(_ =>
                {
                    // 離したときの拡縮処理
                    _rectTransform.DOScale(1f, 0.24f).SetEase(Ease.OutCubic);
                })
                .AddTo(this);

            _button.OnButtonClicked
                .AsObservable()
                .Subscribe(_ =>
            {
                // ステージ選択画面に進み、完了後にキャンバスグループの設定を変更する
                _stageSelectCanvas.DOFade(1.0f, _fadeTime).OnComplete(ValidUI);

                _titleCanvas.interactable = false;
                _titleCanvas.blocksRaycasts = false;
                _titleCanvas.DOFade(0.0f, _fadeTime);
            })
            .AddTo(this);
        }

        /// <summary>
        /// キャンバスグループの設定を有効にする
        /// </summary>
        private void ValidUI()
        {
            // ボタンを有効にする
            _stageSelectCanvas.interactable = true;
            // タイトルとステージセレクトを透明度で入れ替えている影響
            // でボタンが重なって反応しない状態を回避する
            _stageSelectCanvas.blocksRaycasts = true;
        }
    }
}

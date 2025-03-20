using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using UnityEngine.UI;

namespace SleepingAnimals
{
    /// <summary>
    /// ゲームスタートボタン処理実装
    /// </summary>
    public class GameStartButtonView : BaseButton
    {
        [SerializeField]
        [Header("遷移後のシーン名")]
        private string _sceneName;

        [SerializeField]
        [Range(0.1f, 2.0f)]
        [Header("遷移時間")]
        private float _fadeTime;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            _button.OnButtonClicked
                .AsObservable()
                .Subscribe(_ =>
                {
                    SoundManager.Instance.PlaySE(SoundManager.systemDeside);
                    SceneTransitionManager.Instance.ChangeScene(_sceneName);
                    SoundManager.Instance.StopBGM(SoundManager.titleBGM);
                })
            .AddTo(this);
        }
    }
}

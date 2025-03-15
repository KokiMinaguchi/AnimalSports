using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// ホームボタンの処理実装
    /// </summary>
    public class HomeButton : BaseButton
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            _button.OnButtonClicked.AsObservable().Subscribe(button =>
            {
                // タイトル画面に戻る
                SceneTransitionManager.Instance.ChangeScene("TitleScene_bingo");
            })
            .AddTo(this);
        }
    }
}

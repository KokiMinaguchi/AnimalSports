using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class RetryButton : BaseButton
    {

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            _button.OnButtonClicked.AsObservable().Subscribe(button =>
            {
                // タイトル画面に戻り、完了後にキャンバスグループの設定を変更する
                //_stageSelectCanvas.interactable = false;
                //_stageSelectCanvas.blocksRaycasts = false;
                //_stageSelectCanvas.DOFade(0.0f, _fadeTime);
                //_titleCanvas.DOFade(1.0f, _fadeTime).OnComplete(ValidUI);

                SceneTransitionManager.Instance.ChangeScene("GameScene");
            })
            .AddTo(this);
        }
    }
}

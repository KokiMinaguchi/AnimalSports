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

            _button.OnButtonClicked.AsObservable().Subscribe(_ =>
            {
                SoundManager.Instance.PlaySE(SoundManager.systemDeside);
                SceneTransitionManager.Instance.ChangeScene("GameScene");
            })
            .AddTo(this);
        }
    }
}

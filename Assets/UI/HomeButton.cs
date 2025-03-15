using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// �z�[���{�^���̏�������
    /// </summary>
    public class HomeButton : BaseButton
    {
        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();

            _button.OnButtonClicked.AsObservable().Subscribe(button =>
            {
                // �^�C�g����ʂɖ߂�
                SceneTransitionManager.Instance.ChangeScene("TitleScene_bingo");
            })
            .AddTo(this);
        }
    }
}

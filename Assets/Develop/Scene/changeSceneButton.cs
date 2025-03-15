using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;

namespace SleepingAnimals
{
    public class changeSceneButton : BaseButton
    {
        public string _sceneName;
        // Start is called before the first frame update
        protected override void Start()
        {
            _button = GetComponent<CustomButton>();
            _button.OnButtonClicked
                .AsObservable()
                .Subscribe(_ =>
                {
                    SceneTransitionManager.Instance.ChangeScene(_sceneName);
                })
            .AddTo(this);
        }
    }
}

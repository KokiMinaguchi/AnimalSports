using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField]
        private ScoreModel _scoreModel;

        [SerializeField]
        private ScoreView _scoreView;

        // Start is called before the first frame update
        void Start()
        {
            _scoreModel.Score.Subscribe(score =>
            {
                //score = _scoreModel.Score.CurrentValue;
                _scoreView.SetText(_scoreModel.PreScore.CurrentValue, _scoreModel.Score.CurrentValue);

            }).AddTo(this);
        }
    }
}

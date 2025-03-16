using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField]
        [Header("スコア処理")]
        private ScoreModel _scoreModel;

        [SerializeField]
        [Header("スコア表示")]
        private ScoreView _scoreView;

        // Start is called before the first frame update
        void Start()
        {
            _scoreModel.Score.Subscribe(score =>
            {
                // スコアをセット
                _scoreView.SetText(_scoreModel.PreScore.CurrentValue, _scoreModel.Score.CurrentValue);

            }).AddTo(this);
        }
    }
}

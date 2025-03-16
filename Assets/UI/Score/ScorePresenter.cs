using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class ScorePresenter : MonoBehaviour
    {
        [SerializeField]
        [Header("�X�R�A����")]
        private ScoreModel _scoreModel;

        [SerializeField]
        [Header("�X�R�A�\��")]
        private ScoreView _scoreView;

        // Start is called before the first frame update
        void Start()
        {
            _scoreModel.Score.Subscribe(score =>
            {
                // �X�R�A���Z�b�g
                _scoreView.SetText(_scoreModel.PreScore.CurrentValue, _scoreModel.Score.CurrentValue);

            }).AddTo(this);
        }
    }
}

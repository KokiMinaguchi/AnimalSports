using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// �X�R�A����
    /// </summary>
    public class ScoreModel : MonoBehaviour
    {
        public readonly int _maxScore = 999;
        // �X�R�A�̃J�E���g�A�b�v�A�j���[�V�������������邽�߂Ɍ��݂̃X�R�A�ƒ��O�̃X�R�A���Ǘ�
        private readonly SerializableReactiveProperty<int> _score = new(0);
        private readonly SerializableReactiveProperty<int> _preScore = new(0);
        public ReadOnlyReactiveProperty<int> Score => _score;
        public ReadOnlyReactiveProperty<int> PreScore => _preScore;

        /// <summary>
        /// �X�R�A���Z�i���_����Ƃ��̓}�C�i�X�̒l������j
        /// </summary>
        /// <param name="value">���_</param>
        public void AddScore(int value)
        {
            _preScore.Value = _score.Value;
            _score.Value = Mathf.Clamp(_score.Value + value, 0, _maxScore);
        }
        /// <summary>
        /// �X�R�A���Z�b�g
        /// </summary>
        public void ResetScore()
        {
            _preScore.Value = 0;
            _score.Value = 0;
        }
    }
}

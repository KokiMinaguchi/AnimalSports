using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using DG.Tweening;
using System;
using UnityEngine.UI;

namespace SleepingAnimals
{
    /// <summary>
    /// �J�E���g�A�b�v�A�j���[�V���������̂��߂̃J�X�^���g�D�C�[��
    /// </summary>
    public static class DOTweenEx
    {
        public static Tweener DOTextInt(this TextMeshProUGUI text, int initialValue, int finalValue, float duration, Func<int, string> convertor)
        {
            return DOTween.To(
                 () => initialValue,
                 it => text.text = convertor(it),
                 finalValue,
                 duration
             );
        }

        public static Tweener DOTextInt(this TextMeshProUGUI text, int initialValue, int finalValue, float duration)
        {
            return DOTextInt(text, initialValue, finalValue, duration, it => it.ToString());
        }
    }
    
    /// <summary>
    /// �X�R�A�\��
    /// </summary>
    public class ScoreView : MonoBehaviour
    {
        [SerializeField]
        [Header("�X�R�A�e�L�X�g")]
        private TextMeshProUGUI _scoreText;

        [SerializeField]
        [Range(0.1f, 5.0f)]
        [Header("�J�E���g�A�b�v�A�j���[�V�����̃X�s�[�h")]
        private float _countUpAnimDuration;

        /// <summary>
        /// �X�R�A���e�L�X�g������ɂ��ĕ\��
        /// </summary>
        /// <param name="currentScore">���݂̃X�R�A</param>
        /// <param name="value">���Z��̃X�R�A</param>
        public void SetText(int currentScore, int value)
        {
            _scoreText.DOTextInt(currentScore, value, _countUpAnimDuration);
        }
    }
}

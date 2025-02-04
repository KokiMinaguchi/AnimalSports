using CommonViewParts;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SleepingAnimals
{
    /// <summary>
    /// 音量ボタンUIの切り替え
    /// </summary>
    [RequireComponent(typeof(CustomButton))]
    public class audioIconUI : MonoBehaviour
    {
        private CustomButton _button;
        private Image _image;

        public bool _isPlayAudio = true;

        [SerializeField]
        [Header("音量アイコンON画像")]
        private Sprite _audioON;

        [SerializeField]
        [Header("音量アイコンOFF画像")]
        private Sprite _audioOFF;

        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<CustomButton>();
            _image = GetComponent<Image>();

            _button.OnButtonClicked.AsObservable().Subscribe(_ =>
            {
                // 音量アイコン画像の切り替え
                _isPlayAudio ^= true;
                if(_isPlayAudio)
                {
                    _image.sprite = _audioON;
                }
                else
                {
                    _image.sprite = _audioOFF;
                }
            }
            ).AddTo(this);
        }
    }
}

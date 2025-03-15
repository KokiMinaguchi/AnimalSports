using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace SleepingAnimals
{
    /// <summary>
    /// ���ʃ{�^��UI�̐؂�ւ�
    /// </summary>
    [RequireComponent(typeof(CustomButton))]
    public class audioIconUI : BaseButton
    {
        private Image _image;

        public bool _isPlayAudio = true;

        [SerializeField]
        [Header("���ʃA�C�R��ON�摜")]
        private Sprite _audioON;

        [SerializeField]
        [Header("���ʃA�C�R��OFF�摜")]
        private Sprite _audioOFF;

        // Start is called before the first frame update
        protected override void Start()
        {
            base.Start();
            _image = GetComponent<Image>();

            _button.OnButtonClicked.AsObservable().Subscribe(_ =>
            {
                // ���ʃA�C�R���摜�̐؂�ւ�
                _isPlayAudio ^= true;
                if(_isPlayAudio)
                {
                    _image.sprite = _audioON;
                }
                else
                {
                    _image.sprite = _audioOFF;
                }
            })
            .AddTo(this);
        }
    }
}

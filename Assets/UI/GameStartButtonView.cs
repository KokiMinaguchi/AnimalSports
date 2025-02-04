using CommonViewParts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using R3;
using DG.Tweening;

namespace SleepingAnimals
{
    /// <summary>
    /// �Q�[���X�^�[�g�{�^����������
    /// </summary>
    [RequireComponent(typeof(CustomButton))]
    public class GameStartButtonView : MonoBehaviour
    {
        private CustomButton _button;

        [SerializeField]
        [Header("�^�C�g����ʃL�����o�X")]
        private GameObject _titleCanvas;

        [SerializeField]
        [Header("�X�e�[�W�I����ʃL�����o�X")]
        private GameObject _stageSelectCanvas;

        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<CustomButton>();

            _button.OnButtonClicked.AsObservable().Subscribe(button =>
            {
                // �X�e�[�W�I����ʂɐi��
                _titleCanvas.SetActive(false);
                _stageSelectCanvas.SetActive(true);
            })
            .AddTo(this);
        }
    }
}

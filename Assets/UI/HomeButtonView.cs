using CommonViewParts;
using R3;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

namespace SleepingAnimals
{
    /// <summary>
    /// �z�[���{�^���̏�������
    /// </summary>
    [RequireComponent(typeof(CustomButton))]
    public class HomeButtonView : MonoBehaviour
    {
        private CustomButton _button;

        [SerializeField]
        [Header("�X�e�[�W�I���L�����o�X")]
        private GameObject _stageSelectCanvas;

        [SerializeField]
        [Header("�^�C�g���L�����o�X")]
        private GameObject _titleCanvas;

        // Start is called before the first frame update
        void Start()
        {
            _button = GetComponent<CustomButton>();

            _button.OnButtonClicked.AsObservable().Subscribe(button =>
            {
                // �^�C�g����ʂɖ߂�
                _stageSelectCanvas.gameObject.SetActive(false);
                _titleCanvas.gameObject.SetActive(true);
            })
            .AddTo(this);
        }
    }
}

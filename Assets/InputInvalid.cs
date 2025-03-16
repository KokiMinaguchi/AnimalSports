using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// ���͂𖳌��ɂ��鏈������
    /// </summary>
    public class InputInvalid : MonoBehaviour
    {
        [SerializeField]
        [Header("�v���C���[�̃��W�b�h�{�f�B")]
        private Rigidbody _rb;

        [SerializeField]
        private GameObject _eventSystem;

        /// <summary>
        /// ���͂𖳌��ɂ��ăv���C���[���ړ����Ȃ��悤�ɂ���
        /// </summary>
        public void Invalid()
        {
            // �v���C���[�̃t���[�Y�|�W�V������ON�A�C�x���g�V�X�e�����\���ɂ��ē��͂𖳌��ɂ���
            _rb.constraints = RigidbodyConstraints.FreezeRotation  //Rotation��S�ăI��
            | RigidbodyConstraints.FreezePosition;  //Position��S�ăI��
            _eventSystem.SetActive(false);
        }
    }
}

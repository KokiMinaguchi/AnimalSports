using SleepingAnimals.Core;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// �X�e�[�W�p�l����_���A��������
    /// </summary>
    public class LightUpPanel : MonoBehaviour
    {
        [SerializeField]
        private Tag _tag;

        [SerializeField]
        [Header("�_���O�̃}�e���A��")]
        private Color _normalColor = new Color(0.47f, 0.47f, 0.47f, 1.0f);

        [SerializeField]
        [Header("�_����̃}�e���A��")]
        private Color _lightUpColor;

        // �p�l�����_�����Ă��邩
        private bool _isLightUp = false;

        public bool IsLightUp { get { return _isLightUp; } }

        //private void Start()
        //{
        //    // debug
        //    this.GetComponent<MeshRenderer>().material.color = _lightUpColor;
        //}

        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.CompareTag(_tag.Name))
            {
                Material material = this.GetComponent<MeshRenderer>().material;
                // �p�l���𓥂񂾂�}�e���A����ύX
                if (_isLightUp)
                {
                    // �p�l��������
                    material.color = _normalColor;
                    material.SetFloat("_Metallic", 0.0f);
                    Debug.Log("lightnormal");
                    _isLightUp = false;
                }
                else
                {
                    // �p�l����_��
                    material.color = _lightUpColor;
                    //material.SetFloat("_Metallic", 1.0f);
                    _isLightUp = true;
                }
            }
        }
    }
}

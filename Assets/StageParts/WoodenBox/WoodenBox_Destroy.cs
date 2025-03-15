using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using DG.Tweening;

namespace SleepingAnimals
{
    public class WoodenBox_Destroy : MonoBehaviour
    {
        [SerializeField]
        [Header("�؂̔j�Ђ��t�F�[�h���ď����܂ł̎���")]
        [Range(1.0f,5.0f)]
        float _destroyTime;
        private void OnEnable()
        {
            // �A�N�e�B�u���ɔj��
            destroyObject();
        }

        public void destroyObject()
        {
            var random = new System.Random();
            var min = -5;
            var max = 5;
            // �؂̔j�ЃI�u�W�F�N�g�ɔj��̕���������������
            gameObject.GetComponentsInChildren<Rigidbody>().ToList().ForEach(r => {
                r.isKinematic = false;
                r.transform.SetParent(null);
                var vect = new Vector3(random.Next(min, max), random.Next(0, max), random.Next(min, max));
                r.AddForce(vect, ForceMode.Impulse);
                //r.AddExplosionForce(10f, transform.position, 10f);
                r.AddTorque(vect, ForceMode.Impulse);
                // �j�Ђ��t�F�[�h�����Ă������
                r.gameObject.GetComponent<Renderer>().material.DOFade(0.0f, _destroyTime);
                Destroy(r.gameObject, _destroyTime);
            });
            Destroy(gameObject);
        }
    }
}

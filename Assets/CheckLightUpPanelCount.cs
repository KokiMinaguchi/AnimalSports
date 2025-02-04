using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// �p�l���������_�����Ă��邩�J�E���g����
    /// </summary>
    public class CheckLightUpPanelCount : MonoBehaviour
    {
        [SerializeField]
        [Header("�X�e�[�W")]
        private GameObject _stage;

        private LightUpPanel[] _lightUpPanels;

        [SerializeField]
        [Header("�N���A���o�L�����o�X")]
        private GameObject _resultCanvas;

        // Start is called before the first frame update
        void Start()
        {
            // �X�e�[�W�p�l���̃X�N���v�g�����ׂĎ擾
            _lightUpPanels = new LightUpPanel[_stage.transform.childCount];
            for (int panelCnt = 0; panelCnt < _stage.transform.childCount; ++panelCnt)
            {
                // �X�e�[�W�p�l���̎q�I�u�W�F�N�g�ɃX�N���v�g�����Ă���̂łQ��GetChild()���Ă���
                _lightUpPanels[panelCnt] = _stage.transform.GetChild(panelCnt).GetChild(0).GetComponent<LightUpPanel>();
            }
        }

        // Update is called once per frame
        void Update()
        {
            var panelCnt = _lightUpPanels.Length;
            Debug.Log(panelCnt);
            
            // ���ׂẴp�l�����_�����Ă�����N���A
            // TODO UniRx�Ŏ���
            foreach (var panel in _lightUpPanels)
            {
                Debug.Log(panel);
                if(panel.IsLightUp)
                {
                    panelCnt--;
                    Debug.Log(panel.IsLightUp);
                }
            }
            if(panelCnt <= 0)
            {
                _resultCanvas.SetActive(true);
                Debug.Log("Clear");
            }
        }
    }
}

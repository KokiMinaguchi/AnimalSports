using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// ���U���g��ʗp�X�R�A�\��
    /// </summary>
    public class DisplayResultScore : MonoBehaviour
    {
        [SerializeField]
        private GameData _gameData;

        // Start is called before the first frame update
        void Start()
        {
            // �ŏI�I�ȃX�R�A��\������
            GetComponent<TextMeshProUGUI>().text = _gameData.Score.ToString();
        }
    }
}

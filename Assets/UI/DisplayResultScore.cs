using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace SleepingAnimals
{
    /// <summary>
    /// リザルト画面用スコア表示
    /// </summary>
    public class DisplayResultScore : MonoBehaviour
    {
        [SerializeField]
        private GameData _gameData;

        // Start is called before the first frame update
        void Start()
        {
            // 最終的なスコアを表示する
            GetComponent<TextMeshProUGUI>().text = _gameData.Score.ToString();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace SleepingAnimals
{
    public class PlayBGM : MonoBehaviour
    {
        [SerializeField]
        private string _bgmName;

        [SerializeField]
        private string[] _playBgmName;

        // Start is called before the first frame update
        void Start()
        {
            SoundManager.Instance.PlayBGM(_bgmName);
            SoundManager.Instance.StopBGM(_playBgmName[0]);
            SoundManager.Instance.StopBGM(_playBgmName[1]);
        }
    }
}

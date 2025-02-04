using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SleepingAnimals
{
    public class SceneTransitionManager : SingletonMonoBehaviour<SceneTransitionManager>
    {
        // Start is called before the first frame update
        void Start()
        {
            SceneManager.LoadSceneAsync("TitleScene", LoadSceneMode.Additive);
        }

        private void UnloadScene()
        {

        }

        public void LoadScene()
        {
            
        }

        public void ReloadScene()
        {

        }

        public void ChangeScene(string nextSceneName)
        {
            string activeScene = SceneManager.GetActiveScene().name;
            // シーンのロード
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
            //Debug.Log(nextSceneName + "completed");

            // シーンのアンロード
            SceneManager.UnloadSceneAsync(activeScene);
        }
    }
}

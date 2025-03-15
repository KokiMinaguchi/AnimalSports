using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace SleepingAnimals
{
    public class SceneTransitionManager : SingletonMonoBehaviour<SceneTransitionManager>
    {
        [SerializeField]
        IrisShotController _fadeControll;
        [SerializeField]
        private float _fadeTime;

        private string _currentScene;
        // Start is called before the first frame update
        void Start()
        {
            _currentScene = "TitleScene_bingo";
            SceneManager.LoadSceneAsync(_currentScene, LoadSceneMode.Additive);
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

        public async void ChangeScene(string nextSceneName)
        {
            // �ȈՓI�Ɏ���
            string activeScene = SceneManager.GetActiveScene().name;
            // �t�F�[�h����
            _fadeControll.IrisOut();
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeTime));
            // �V�[���̃A�����[�h
            SceneManager.UnloadSceneAsync(_currentScene);
            // �V�[���̃��[�h
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
            _currentScene = nextSceneName;
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeTime));
            _fadeControll.IrisIn();
            //Debug.Log(nextSceneName + "completed");

            
        }
    }
}

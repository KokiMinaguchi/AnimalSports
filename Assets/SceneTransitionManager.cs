using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace SleepingAnimals
{
    /// <summary>
    /// シーン管理
    /// </summary>
    public class SceneTransitionManager : SingletonMonoBehaviour<SceneTransitionManager>
    {
        [SerializeField]
        IrisShotController _fadeControll;

        [SerializeField]
        [Range(1.0f, 2.0f)]
        private float _fadeTime;

        [SerializeField]
        private string _currentScene;

        // Start is called before the first frame update
        void Start()
        {
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
        /// <summary>
        /// シーン遷移処理（非同期）
        /// </summary>
        /// <param name="nextSceneName">遷移先のシーン名</param>
        public async void ChangeScene(string nextSceneName)
        {
            // 簡易的に実装
            string activeScene = SceneManager.GetActiveScene().name;
            // フェード処理
            _fadeControll.IrisOut();
            // 数秒待ってからシーン遷移する
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeTime));
            // シーンのアンロード
            SceneManager.UnloadSceneAsync(_currentScene);
            // シーンのロード
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
            _currentScene = nextSceneName;
            // 数秒待ってからフェードインする
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeTime));
            _fadeControll.IrisIn();
        }
    }
}

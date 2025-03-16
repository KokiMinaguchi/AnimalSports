using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace SleepingAnimals
{
    /// <summary>
    /// �V�[���Ǘ�
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
        /// �V�[���J�ڏ����i�񓯊��j
        /// </summary>
        /// <param name="nextSceneName">�J�ڐ�̃V�[����</param>
        public async void ChangeScene(string nextSceneName)
        {
            // �ȈՓI�Ɏ���
            string activeScene = SceneManager.GetActiveScene().name;
            // �t�F�[�h����
            _fadeControll.IrisOut();
            // ���b�҂��Ă���V�[���J�ڂ���
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeTime));
            // �V�[���̃A�����[�h
            SceneManager.UnloadSceneAsync(_currentScene);
            // �V�[���̃��[�h
            SceneManager.LoadSceneAsync(nextSceneName, LoadSceneMode.Additive);
            _currentScene = nextSceneName;
            // ���b�҂��Ă���t�F�[�h�C������
            await UniTask.Delay(TimeSpan.FromSeconds(_fadeTime));
            _fadeControll.IrisIn();
        }
    }
}

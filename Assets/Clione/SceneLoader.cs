using System;
using System.Collections;
using Clione.Utility;
using UnityEngine;

namespace Clione
{
    /// <summary>
    /// シーンを読み込む
    /// </summary>
    public class SceneLoader : Singleton<SceneLoader>
    {
        private const string GameObjectName = "[Clione Dispacher]";

        private MonoBehaviour _monoBehaviour;

        private SceneManager _sceneManager;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public void Initialize(SceneManager sceneManager = null)
        {
            if (!GameObject.Find(GameObjectName))
            {
                var gameObject = new GameObject {name = GameObjectName};
                GameObject.DontDestroyOnLoad(gameObject);
                gameObject.AddComponent<ClioneDispatcher>();
                _monoBehaviour = gameObject.GetComponent<MonoBehaviour>();
            }

            SceneManager manager;

            if (sceneManager == null)
            {
                manager = new SceneManager(_monoBehaviour);
            }
            else
            {
                manager = sceneManager;
            }

            _sceneManager = manager;

            _sceneManager.SceneInitialize();
        }

        public void LoadScene(string sceneName) => _monoBehaviour.StartCoroutine(LoadSceneEnumerator(sceneName));

        public void LoadWindow(string windowPath, string screenPath) =>
            _monoBehaviour.StartCoroutine(LoadWindowEnumerator(windowPath, screenPath));

        public void LoadScreen(string screenPath) =>
            _monoBehaviour.StartCoroutine(LoadScreenEnumerator(screenPath));

        public IEnumerator LoadSceneEnumerator(string sceneName)
        {
            yield return _monoBehaviour.StartCoroutine(_sceneManager.LoadScene(sceneName));
        }

        public IEnumerator LoadWindowEnumerator(string windowPath, string screenPath)
        {
            yield return _monoBehaviour.StartCoroutine(_sceneManager.LoadWindow(windowPath, screenPath));
        }

        public IEnumerator LoadScreenEnumerator(string screenPath)
        {
            yield return _monoBehaviour.StartCoroutine(_sceneManager.LoadScreen(screenPath));
        }
    }
}
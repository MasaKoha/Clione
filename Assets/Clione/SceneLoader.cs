using System;
using System.Collections;
using UnityEngine;

namespace Clione
{
    /// <summary>
    /// シーンを読み込む
    /// </summary>
    public class SceneLoader
    {
        private const string GameObjectName = "[Clione Dispacher]";

        public static event Action BeginOpenWindowAction;

        public static event Action EndOpenWindowAction;

        public static event Action BeginCloseWindowAction;

        public static event Action EndCloseWindowAction;

        public static event Action BeginOpenScreenAction;

        public static event Action EndOpenScreenAction;

        public static event Action BeginCloseScreenAction;

        public static event Action EndCloseScreenAction;

        private static SceneLoader _instance;

        private static MonoBehaviour _monoBehaviour;

        private static SceneManager _sceneManager;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public SceneLoader(SceneManager sceneManager = null)
        {
            if (!GameObject.Find(GameObjectName))
            {
                var gameObject = new GameObject {name = GameObjectName};
                GameObject.DontDestroyOnLoad(gameObject);
                _monoBehaviour = gameObject.GetComponent<MonoBehaviour>();
            }

            if (sceneManager == null)
            {
                _sceneManager = new SceneManager(_monoBehaviour);
            }

            _sceneManager = sceneManager;
            SetEvent();
            _instance = this;
        }

        private void SetEvent()
        {
            _sceneManager.CurrentOpenScene.BeginOpenWindowAction += () => BeginOpenWindowAction?.Invoke();
            _sceneManager.CurrentOpenScene.EndOpenWindowAction += () => EndOpenWindowAction?.Invoke();
            _sceneManager.CurrentOpenScene.BeginCloseWindowAction += () => BeginCloseWindowAction?.Invoke();
            _sceneManager.CurrentOpenScene.EndCloseWindowAction += () => EndCloseWindowAction?.Invoke();

            _sceneManager.CurrentOpenWindow.BeginOpenScreenAction += () => BeginOpenScreenAction?.Invoke();
            _sceneManager.CurrentOpenWindow.EndOpenScreenAction += () => EndOpenScreenAction?.Invoke();
            _sceneManager.CurrentOpenWindow.BeginCloseScreenAction += () => BeginCloseScreenAction?.Invoke();
            _sceneManager.CurrentOpenWindow.EndCloseScreenAction += () => EndCloseScreenAction?.Invoke();
        }

        /// <summary>
        /// Scene を読み込む
        /// </summary>
        public static IEnumerator LoadScene(string sceneName)
        {
            yield return _monoBehaviour.StartCoroutine(_sceneManager.LoadScene(sceneName));
        }

        /// <summary>
        /// Window と Screen を読み込む
        /// </summary>
        public static IEnumerator LoadWindow(string windowPath, string screenPath)
        {
            yield return _monoBehaviour.StartCoroutine(_sceneManager.LoadWindow(windowPath, screenPath));
        }

        /// <summary>
        /// Screen を読み込む
        /// </summary>
        public static IEnumerator LoadScreen(string screenPath)
        {
            yield return _monoBehaviour.StartCoroutine(_sceneManager.LoadScreen(screenPath));
        }
    }
}
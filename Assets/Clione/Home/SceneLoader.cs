﻿using System;
using System.Collections;
using Clione.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Clione
{
    /// <summary>
    /// シーンを読み込む
    /// </summary>
    public class SceneLoader : Singleton<SceneLoader>
    {
        private const string GameObjectName = "[Clione MainThread Dispacher]";

        private MonoBehaviour _monoBehaviour;

        private SceneManager _sceneManager;

        public bool IsLoadingScene => _sceneManager.IsLoadingScene;

        /// <summary>
        /// 現在開かれている Scene 名
        /// ※マルチシーン非対応
        /// </summary>
        public string CurrentSceneName => _sceneManager.CurrentSceneName;

        /// <summary>
        /// アプリケーションの初回起動時かどうか
        /// </summary>
        private bool _isFirstBoot = false;

        /// <summary>
        /// 初期化
        /// アプリケーションの初期化時のみ叩かれる
        /// </summary>
        public void Initialize()
        {
            if (_isFirstBoot)
            {
                return;
            }

            if (!GameObject.Find(GameObjectName))
            {
                var gameObject = new GameObject {name = GameObjectName};
                Object.DontDestroyOnLoad(gameObject);
                gameObject.AddComponent<ClioneDispatcher>();
                _monoBehaviour = gameObject.GetComponent<MonoBehaviour>();
            }

            _sceneManager = new SceneManager(_monoBehaviour);
            _sceneManager.SceneInitialize();
            _isFirstBoot = true;
        }

        public void LoadScene(string sceneName, object param = null, Action onComplete = null) =>
            _monoBehaviour.StartCoroutine(LoadSceneEnumerator(sceneName, param, onComplete));

        public void LoadWindow(string windowPath, string screenPath, Action onComplete = null) =>
            _monoBehaviour.StartCoroutine(LoadWindowEnumerator(windowPath, screenPath, onComplete));

        public void LoadScreen(string screenPath, Action onComplete = null) =>
            _monoBehaviour.StartCoroutine(LoadScreenEnumerator(screenPath, onComplete));

        public IEnumerator LoadSceneEnumerator(string sceneName, object param = null, Action onComplete = null)
        {
            yield return _monoBehaviour.StartCoroutine(_sceneManager.LoadScene(sceneName, param, onComplete));
        }

        public IEnumerator LoadWindowEnumerator(string windowPath, string screenPath, Action onComplete = null)
        {
            yield return _monoBehaviour.StartCoroutine(_sceneManager.LoadWindow(windowPath, screenPath, onComplete));
        }

        public IEnumerator LoadScreenEnumerator(string screenPath, Action onComplete = null)
        {
            yield return _monoBehaviour.StartCoroutine(_sceneManager.LoadScreen(screenPath, onComplete));
        }
    }
}
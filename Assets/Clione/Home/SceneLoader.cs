using System;
using System.Collections;
using Clione.Core;
using UnityEngine;

namespace Clione.Home
{
    /// <summary>
    /// シーンを読み込む
    /// </summary>
    public static class SceneLoader
    {
        private static ISceneManager _sceneManager;

        public static bool IsLoadingScene => _sceneManager.IsLoadingScene;

        /// <summary>
        /// 現在開かれている Scene 名
        /// ※マルチシーン非対応
        /// </summary>
        public static string CurrentSceneName => _sceneManager.CurrentSceneName;

        /// <summary>
        /// 初期化
        /// アプリケーションの初期化時に必ず呼ぶこと
        /// </summary>
        public static void Initialize(ISceneManager sceneManager)
        {
            _sceneManager = sceneManager;
            _sceneManager.InitializeSetUp();
        }

        public static void LoadScene(string sceneName, object param = null, Action onComplete = null, Action onFail = null)
        {
            ClioneCore.Run(LoadSceneEnumerator(sceneName, param, onComplete, onFail));
        }

        public static void LoadWindow(string windowPath, string screenPath, Action onComplete = null)
        {
            ClioneCore.Run(LoadWindowEnumerator(windowPath, screenPath, onComplete));
        }

        public static void LoadScreen(string screenPath, Action onComplete = null)
        {
            ClioneCore.Run(LoadScreenEnumerator(screenPath, onComplete));
        }

        private static IEnumerator LoadSceneEnumerator(string sceneName, object param = null, Action onComplete = null, Action onFail = null)
        {
            var loadScene = _sceneManager.LoadSceneEnumerator(sceneName, param, onComplete, onFail);
            while (loadScene.MoveNext())
            {
                yield return null;
            }
        }

        private static IEnumerator LoadWindowEnumerator(string windowPath, string screenPath, Action onComplete = null)
        {
            var loadWindow = _sceneManager.LoadWindowEnumerator(windowPath, screenPath, onComplete);
            while (loadWindow.MoveNext())
            {
                yield return null;
            }
        }

        private static IEnumerator LoadScreenEnumerator(string screenPath, Action onComplete = null)
        {
            var loadScreen = _sceneManager.LoadScreenEnumerator(screenPath, onComplete);
            while (loadScreen.MoveNext())
            {
                yield return null;
            }
        }
    }
}
using System;
using System.Collections;
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

        private static MonoBehaviour Mono => _sceneManager.Mono;

        /// <summary>
        /// 初期化
        /// アプリケーションの初期化時に必ず呼ぶこと
        /// </summary>
        public static void Initialize(ISceneManager sceneManager)
        {
            _sceneManager = sceneManager;
            _sceneManager.InitializeSetUp();
        }

        public static void LoadScene(string sceneName, object param = null, Action onComplete = null) =>
            Mono.StartCoroutine(LoadSceneEnumerator(sceneName, param, onComplete));

        public static void LoadWindow(string windowPath, string screenPath, Action onComplete = null) =>
            Mono.StartCoroutine(LoadWindowEnumerator(windowPath, screenPath, onComplete));

        public static void LoadScreen(string screenPath, Action onComplete = null) =>
            Mono.StartCoroutine(LoadScreenEnumerator(screenPath, onComplete));

        public static IEnumerator LoadSceneEnumerator(string sceneName, object param = null, Action onComplete = null)
        {
            yield return Mono.StartCoroutine(_sceneManager.LoadWindowAndScreenEnumerator(sceneName, param, onComplete));
        }

        public static IEnumerator LoadWindowEnumerator(string windowPath, string screenPath, Action onComplete = null)
        {
            yield return Mono.StartCoroutine(_sceneManager.LoadWindowEnumerator(windowPath, screenPath, onComplete));
        }

        public static IEnumerator LoadScreenEnumerator(string screenPath, Action onComplete = null)
        {
            yield return Mono.StartCoroutine(_sceneManager.LoadScreenEnumerator(screenPath, onComplete));
        }
    }
}
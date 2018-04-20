using System;
using System.Collections;
using UnityEngine;

namespace Clione
{
    /// <summary>
    /// シーンに関する管理クラス
    /// </summary>
    public class SceneManager
    {
        private ScenePresenterBase _currentOpenScene = null;

        /// <summary>
        /// 現在開かれている Scene 名
        /// </summary>
        private static string CurrentSceneName => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        /// <summary>
        /// 現在開かれている Window のパス
        /// </summary>
        private string CurrentWindowPath => _currentOpenScene?.CurrentOpenWindowPath ?? string.Empty;

        /// <summary>
        /// 現在開かれている Screen のパス
        /// </summary>
        private string CurrentScreenPath => _currentOpenScene?.CurrentOpenScreenPath ?? string.Empty;

        /// <summary>
        /// Sceneがロード中かどうか
        /// </summary>
        public bool IsLoadingScene = false;

        private readonly MonoBehaviour _monoBehaviour;

        public SceneManager(MonoBehaviour monoBehaviour)
        {
            _monoBehaviour = monoBehaviour;
        }

        public void SceneInitialize()
        {
            _monoBehaviour.StartCoroutine(SceneInitializeEnumerator());
        }

        /// <summary>
        /// シーンの初期化
        /// </summary>
        public IEnumerator SceneInitializeEnumerator()
        {
            _currentOpenScene = GetCurrentScenePresenter();
            _currentOpenScene.Initialize(null);
            yield return _monoBehaviour.StartCoroutine(_currentOpenScene.InitializeEnumerator());
            _currentOpenScene.InitializeOpenWindowAndScreen();
        }

        /// <summary>
        /// シーンを読み込む
        /// </summary>
        public IEnumerator LoadScene(string loadSceneName, object param = null, Action onComplete = null)
        {
            if (CurrentSceneName != loadSceneName)
            {
                yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadSceneName);
                Resources.UnloadUnusedAssets();
                GC.Collect();
                _currentOpenScene = GetCurrentScenePresenter();
                _currentOpenScene.Initialize(param);
                _currentOpenScene.InitializeOpenWindowAndScreen();
            }

            onComplete?.Invoke();
        }

        /// <summary>
        /// Window と Screen を読み込む
        /// </summary>
        public IEnumerator LoadWindow(string loadWindowPath, string loadScreenPath, object param = null,
            Action onComplete = null)
        {
            yield return _monoBehaviour.StartCoroutine(
                LoadSceneEnumerator(CurrentSceneName, loadWindowPath, loadScreenPath, param, onComplete));
        }

        /// <summary>
        /// Screen を読み込む
        /// </summary>
        public IEnumerator LoadScreen(string loadScreenPath, object param = null, Action onComplete = null)
        {
            yield return _monoBehaviour.StartCoroutine(LoadSceneEnumerator(CurrentSceneName, CurrentWindowPath,
                loadScreenPath, param, onComplete));
        }

        /// <summary>
        /// Scene を読み込む
        /// </summary>
        private IEnumerator LoadSceneEnumerator(string loadSceneName, string loadWindowPath, string loadScreenPath,
            object param, Action onComplete)
        {
            if (IsLoadingScene)
            {
                Debug.Log("loading...");
            }

            yield return new WaitUntil(() => !IsLoadingScene);
            IsLoadingScene = true;

            _currentOpenScene = GetCurrentScenePresenter();

            if (CurrentSceneName != loadSceneName)
            {
                yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadSceneName);
                Resources.UnloadUnusedAssets();
                GC.Collect();
                _currentOpenScene = GetCurrentScenePresenter();
                _currentOpenScene.Initialize(param);
                yield return _monoBehaviour.StartCoroutine(_currentOpenScene.InitializeEnumerator());
                _currentOpenScene.InitializeOpenWindowAndScreen();
            }

            if (CurrentWindowPath != loadWindowPath)
            {
                yield return _monoBehaviour.StartCoroutine(_currentOpenScene.OnCloseScreenEnumerator());
                yield return _monoBehaviour.StartCoroutine(_currentOpenScene.OnCloseWindowEnumerator());
            }

            if (CurrentScreenPath != loadScreenPath)
            {
                yield return _monoBehaviour.StartCoroutine(_currentOpenScene.OnCloseScreenEnumerator());
            }

            yield return _monoBehaviour.StartCoroutine(
                _currentOpenScene.OnOpenWindowEnumerator(loadWindowPath, loadScreenPath, CurrentWindowPath,
                    CurrentScreenPath));

            onComplete?.Invoke();
            IsLoadingScene = false;
        }

        /// <summary>
        /// 現在のシーンのPresenterを取得する
        /// </summary>
        private static ScenePresenterBase GetCurrentScenePresenter()
        {
            return UnityEngine.Object.FindObjectOfType(typeof(ScenePresenterBase)) as ScenePresenterBase;
        }
    }
}
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
        /// <summary>
        /// Model
        /// </summary>
        private readonly SceneManagerModel _model;

        public ScenePresenterBase CurrentOpenScene { get; private set; }

        public WindowPresenterBase CurrentOpenWindow => CurrentOpenScene.CurrentOpenWindow;

        public ScreenPresenterBase CurrentOpenScreen => CurrentOpenWindow.CurrentOpenScreen;

        /// <summary>
        /// 現在開かれている Scene 名
        /// </summary>
        private string CurrentSceneName => _model.CurrentSceneName;

        /// <summary>
        /// 現在開かれている Window のパス
        /// </summary>
        private string CurrentWindowPath => _model.CurrentWindowPath;

        /// <summary>
        /// 現在開かれている Screen のパス
        /// </summary>
        private string CurrentScreenPath => _model.CurrentScreenPath;

        /// <summary>
        /// Sceneがロード中かどうか
        /// </summary>
        public bool IsLoadingScene = false;

        private readonly MonoBehaviour _monoBehaviour;

        public SceneManager(MonoBehaviour monoBehaviour)
        {
            _model = new SceneManagerModel();
            _monoBehaviour = monoBehaviour;
        }

        /// <summary>
        /// シーンの初期化
        /// </summary>
        public IEnumerator SceneInitialize()
        {
            var currentScene = GetCurrentScenePresenter();
            currentScene.Initialize(null);
            yield return _monoBehaviour.StartCoroutine(currentScene.InitializeEnumerator());
            currentScene.InitializeOpenWindowAndScreen();
        }

        /// <summary>
        /// シーンを読み込む
        /// </summary>
        public IEnumerator LoadScene(string loadSceneName, object param = null, Action onComplete = null)
        {
            if (_model.CurrentSceneName != loadSceneName)
            {
                yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadSceneName);
                Resources.UnloadUnusedAssets();
                GC.Collect();
                var loadScenePresenter = GetCurrentScenePresenter();
                loadScenePresenter.Initialize(param);
                loadScenePresenter.InitializeOpenWindowAndScreen();
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

            ScenePresenterBase loadScenePresenter = GetCurrentScenePresenter();

            if (_model.CurrentSceneName != loadSceneName)
            {
                yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadSceneName);
                Resources.UnloadUnusedAssets();
                GC.Collect();
                loadScenePresenter = GetCurrentScenePresenter();
                loadScenePresenter.Initialize(param);
                yield return _monoBehaviour.StartCoroutine(loadScenePresenter.InitializeEnumerator());
                loadScenePresenter.InitializeOpenWindowAndScreen();
            }

            if (_model.CurrentScreenPath != loadScreenPath)
            {
                yield return _monoBehaviour.StartCoroutine(loadScenePresenter.OnCloseScreenEnumerator());
            }

            if (_model.CurrentWindowPath != loadWindowPath)
            {
                yield return _monoBehaviour.StartCoroutine(loadScenePresenter.OnCloseWindowEnumerator());
            }

            yield return _monoBehaviour.StartCoroutine(
                loadScenePresenter.OnOpenWindowEnumerator(loadWindowPath, loadScreenPath, CurrentWindowPath,
                    CurrentScreenPath));
            _model.SetCurrentWindowPath(loadWindowPath);
            _model.SetCurrentScreenPath(loadScreenPath);

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
using System;
using System.Collections;
using Clione.Utility;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Clione.Home
{
    /// <summary>
    /// シーンに関する管理クラス
    /// </summary>
    public class ClioneSceneManager : ISceneManager
    {
        private SceneBase _currentOpenScene = null;

        private const string GameObjectName = "[Clione MainThread Dispacher]";

        /// <summary>
        /// 現在開かれている Scene 名
        /// ※マルチシーン非対応
        /// </summary>
        public string CurrentSceneName => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        /// <summary>
        /// 現在開かれている Window のパス
        /// </summary>
        public string CurrentWindowPath => _currentOpenScene?.CurrentOpenWindowPath ?? string.Empty;

        /// <summary>
        /// 現在開かれている Screen のパス
        /// </summary>
        public string CurrentScreenPath => _currentOpenScene?.CurrentOpenScreenPath ?? string.Empty;

        private bool _isLoadingScene = false;

        public bool IsLoadingScene => _isLoadingScene;

        private readonly MonoBehaviour _mono;

        public MonoBehaviour Mono => _mono;

        public ClioneSceneManager()
        {
            var gameObject = new GameObject {name = GameObjectName};
            Object.DontDestroyOnLoad(gameObject);
            gameObject.AddComponent<ClioneDispatcher>();
            _mono = gameObject.GetComponent<MonoBehaviour>();
        }

        public virtual void InitializeSetUp()
        {
            // はじめのシーンに移る前にやっておきたい処理をここで記述する
            // 例) 様々な Manager クラスの初期化など
        }

        /// <summary>
        /// シーンを読み込む
        /// </summary>
        public IEnumerator LoadSceneEnumerator(string loadSceneName, object param = null, Action onComplete = null)
        {
            if (CurrentSceneName != loadSceneName)
            {
                yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadSceneName);
                Resources.UnloadUnusedAssets();
                GC.Collect();
                yield return _mono.StartCoroutine(InitializeSceneEnumerator(param));
            }

            onComplete?.Invoke();
        }

        private IEnumerator InitializeSceneEnumerator(object param)
        {
            _currentOpenScene = GetCurrentSceneBase();
            yield return _mono.StartCoroutine(_currentOpenScene.InitializeEnumerator(param));
        }

        /// <summary>
        /// Window と Screen を読み込む
        /// </summary>
        public virtual IEnumerator LoadWindowEnumerator(string loadWindowPath, string loadScreenPath,
            Action onComplete = null)
        {
            yield return _mono.StartCoroutine(LoadSceneEnumerator(CurrentSceneName, loadWindowPath, loadScreenPath, null, onComplete));
        }

        /// <summary>
        /// Screen を読み込む
        /// </summary>
        public virtual IEnumerator LoadScreenEnumerator(string loadScreenPath, Action onComplete = null)
        {
            yield return _mono.StartCoroutine(LoadSceneEnumerator(CurrentSceneName, CurrentWindowPath, loadScreenPath, null, onComplete));
        }

        /// <summary>
        /// Scene を読み込む
        /// </summary>
        private IEnumerator LoadSceneEnumerator(string loadSceneName, string loadWindowPath, string loadScreenPath, object param, Action onComplete)
        {
            if (_isLoadingScene)
            {
                Debug.Log("loading...");
            }

            yield return new WaitUntil(() => !_isLoadingScene);
            _isLoadingScene = true;

            _currentOpenScene = GetCurrentSceneBase();

            if (CurrentSceneName != loadSceneName)
            {
                yield return UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadSceneName);
                Resources.UnloadUnusedAssets();
                GC.Collect();
                _currentOpenScene = GetCurrentSceneBase();
                yield return _mono.StartCoroutine(_currentOpenScene.InitializeEnumerator(param));
            }

            if (CurrentWindowPath != loadWindowPath)
            {
                yield return _mono.StartCoroutine(_currentOpenScene.OnCloseScreenEnumerator());
                yield return _mono.StartCoroutine(_currentOpenScene.OnCloseWindowEnumerator());
            }

            if (CurrentScreenPath != loadScreenPath)
            {
                yield return _mono.StartCoroutine(_currentOpenScene.OnCloseScreenEnumerator());
            }

            yield return _mono.StartCoroutine(_currentOpenScene.OnOpenWindowEnumerator(loadWindowPath, loadScreenPath, CurrentWindowPath, CurrentScreenPath));

            onComplete?.Invoke();
            _isLoadingScene = false;
        }

        /// <summary>
        /// 現在のシーンの SceneBase を取得する
        /// </summary>
        private static SceneBase GetCurrentSceneBase()
        {
            var sceneBase = UnityEngine.Object.FindObjectOfType(typeof(SceneBase)) as SceneBase;

            if (sceneBase == null)
            {
                Debug.LogError("現在開いているシーンに SceneBase を継承した GameObject が存在しません。ヒエラルキー上を確認してください。");
                return null;
            }

            return sceneBase;
        }
    }
}
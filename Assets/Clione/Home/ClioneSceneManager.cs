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
        public IEnumerator LoadSceneEnumerator(string loadSceneName,
            object param = null,
            Action onComplete = null,
            Action onFail = null)
        {
            if (_isLoadingScene)
            {
                Debug.Log($"Load 中に LoadScene が二重で呼ばれたので、あとに Load したほうをキャンセルしました。\n Target LoadScene Name : {loadSceneName}");
                onFail?.Invoke();
                yield break;
            }

            _isLoadingScene = true;

            if (CurrentSceneName != loadSceneName)
            {
                var loadSceneAsync = UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(loadSceneName);

                while (!loadSceneAsync.isDone)
                {
                    yield return null;
                }

                Resources.UnloadUnusedAssets();
                GC.Collect();

                var initialize = InitializeSceneEnumerator(param);
                while (initialize.MoveNext())
                {
                    yield return null;
                }
            }

            onComplete?.Invoke();
            _isLoadingScene = false;
        }

        private IEnumerator InitializeSceneEnumerator(object param)
        {
            _currentOpenScene = GetCurrentSceneBase();
            var initialize = _currentOpenScene.InitializeEnumerator(param);
            while (initialize.MoveNext())
            {
                yield return null;
            }
        }

        /// <summary>
        /// Window と Screen を読み込む
        /// </summary>
        public virtual IEnumerator LoadWindowEnumerator(
            string loadWindowPath,
            string loadScreenPath,
            Action onComplete = null)
        {
            var loadWindowAndScreen = LoadWindowAndScreenEnumerator(loadWindowPath, loadScreenPath, null, onComplete);
            while (loadWindowAndScreen.MoveNext())
            {
                yield return null;
            }
        }

        /// <summary>
        /// Screen を読み込む
        /// </summary>
        public virtual IEnumerator LoadScreenEnumerator(string loadScreenPath, Action onComplete = null)
        {
            var loadWindowAndScreen = LoadWindowAndScreenEnumerator(CurrentWindowPath, loadScreenPath, null, onComplete);
            while (loadWindowAndScreen.MoveNext())
            {
                yield return null;
            }
        }

        /// <summary>
        /// Scene を読み込む
        /// </summary>
        private IEnumerator LoadWindowAndScreenEnumerator(
            string loadWindowPath,
            string loadScreenPath,
            object param,
            Action onComplete)
        {
            _currentOpenScene = GetCurrentSceneBase();

            if (CurrentWindowPath != loadWindowPath)
            {
                var onCloseScreen = _currentOpenScene.OnCloseScreenEnumerator();
                while (onCloseScreen.MoveNext())
                {
                    yield return null;
                }

                var onOpenScene = _currentOpenScene.OnCloseWindowEnumerator();

                while (onOpenScene.MoveNext())
                {
                    yield return null;
                }
            }

            if (CurrentScreenPath != loadScreenPath)
            {
                var onCloseScreen = _currentOpenScene.OnCloseScreenEnumerator();
                while (onCloseScreen.MoveNext())
                {
                    yield return null;
                }
            }

            var onOpenWindow = _currentOpenScene.OnOpenWindowEnumerator(loadWindowPath, loadScreenPath, CurrentWindowPath, CurrentScreenPath);
            while (onOpenWindow.MoveNext())
            {
                yield return null;
            }

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
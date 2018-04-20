using System;
using System.Collections;
using UnityEngine;

namespace Clione
{
    /// <summary>
    /// Scene Presenter の Base クラス
    /// </summary>
    public abstract class ScenePresenterBase : MonoBehaviour
    {
        /// <summary>
        /// Window を生成するルートオブジェクトの Transform
        /// </summary>
        [SerializeField] private Transform _windowRootTransform;

        /// <summary>
        /// Window が読み込まれ始めたときのイベント
        /// </summary>
        public event Action BeginOpenWindowAction;

        /// <summary>
        /// Window が読み込みおわったときに呼ばれるイベント
        /// </summary>
        public event Action EndOpenWindowAction;

        public event Action BeginCloseWindowAction;

        public event Action EndCloseWindowAction;

        /// <summary>
        /// 現在開かれている Window
        /// </summary>
        public WindowPresenterBase CurrentOpenWindow { get; private set; }

        /// <summary>
        /// 現在開かれている Screen
        /// </summary>
        public ScreenPresenterBase CurrentOpenPresenter => CurrentOpenWindow.CurrentOpenScreen;

        /// <summary>
        /// 現在開かれている Window の GameObject
        /// </summary>
        private GameObject _currentWindowGameObject;

        /// <summary>
        /// 初期化
        /// </summary>
        public abstract void Initialize(object param);

        /// <summary>
        /// 非同期の初期化処理
        /// </summary>
        public virtual IEnumerator InitializeEnumerator()
        {
            yield break;
        }

        /// <summary>
        /// 初期に開く画面
        /// </summary>
        public abstract void InitializeOpenWindowAndScreen();

        /// <summary>
        /// Window を開く
        /// </summary>
        public IEnumerator OnOpenWindowEnumerator(string windowPrefabPath, string screenPrefabPath,
            string currentWindowPath, string currentScreenPath)
        {
            BeginOpenWindowAction?.Invoke();

            if (_currentWindowGameObject == null)
            {
                if (currentWindowPath != windowPrefabPath)
                {
                    var prefab = Resources.Load<GameObject>(windowPrefabPath);
                    _currentWindowGameObject = Instantiate(prefab);
                    _currentWindowGameObject.transform.SetParent(_windowRootTransform.transform, false);
                    CurrentOpenWindow = _currentWindowGameObject.GetComponent<WindowPresenterBase>();
                    CurrentOpenWindow.Initialize();
                }
            }

            yield return StartCoroutine(CurrentOpenWindow.OnBeforeOpenWindowEnumerator());
            yield return StartCoroutine(CurrentOpenWindow.OnOpenWindowEnumerator(screenPrefabPath, currentScreenPath));
            yield return StartCoroutine(CurrentOpenWindow.OnAfterOpenWindowEnumerator());

            EndOpenWindowAction?.Invoke();
        }

        /// <summary>
        /// Windowを閉じる
        /// </summary>
        public IEnumerator OnCloseWindowEnumerator()
        {
            if (CurrentOpenWindow == null)
            {
                yield break;
            }

            BeginCloseWindowAction?.Invoke();
            yield return StartCoroutine(CurrentOpenWindow.OnBeforeCloseWindowEnumerator());
            yield return StartCoroutine(CurrentOpenWindow.OnCloseWindowEnumerator());
            yield return StartCoroutine(CurrentOpenWindow.OnAfterCloseWindowEnumerator());
            DestroyImmediate(_currentWindowGameObject);
            _currentWindowGameObject = null;
            EndCloseWindowAction?.Invoke();
        }

        /// <summary>
        /// Screen を閉じる
        /// </summary>
        public IEnumerator OnCloseScreenEnumerator()
        {
            if (CurrentOpenWindow == null)
            {
                yield break;
            }

            if (CurrentOpenWindow.CurrentOpenScreen == null)
            {
                yield break;
            }

            yield return StartCoroutine(CurrentOpenWindow.OnCloseScreenEnumerator());
            DestroyImmediate(CurrentOpenWindow.CurrentOpenScreenPrefab);
            _currentWindowGameObject = null;
        }
    }
}
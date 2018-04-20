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
        /// 現在開かれている Window
        /// </summary>
        private WindowPresenterBase _currentOpenWindow;

        public string CurrentOpenWindowPath => _currentOpenWindow?.WindowPath ?? string.Empty;

        public string CurrentOpenScreenPath => _currentOpenWindow?.CurrentOpenScreenPath ?? string.Empty;

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
        public IEnumerator OnOpenWindowEnumerator(string nextWindowPath, string nextScreenPath,
            string currentWindowPath, string currentScreenPath)
        {
            if (currentWindowPath != nextWindowPath)
            {
                var prefab = Resources.Load<GameObject>(nextWindowPath);
                _currentWindowGameObject = Instantiate(prefab);
                _currentWindowGameObject.transform.SetParent(_windowRootTransform.transform, false);
                _currentOpenWindow = _currentWindowGameObject.GetComponent<WindowPresenterBase>();
                _currentOpenWindow.SetWindowPath(nextWindowPath);
                _currentOpenWindow.Initialize();
            }

            yield return StartCoroutine(_currentOpenWindow.OnBeforeOpenWindowEnumerator());
            yield return StartCoroutine(_currentOpenWindow.OnOpenWindowEnumerator(nextScreenPath, currentScreenPath));
            yield return StartCoroutine(_currentOpenWindow.OnAfterOpenWindowEnumerator());
        }

        /// <summary>
        /// Windowを閉じる
        /// </summary>
        public IEnumerator OnCloseWindowEnumerator()
        {
            if (_currentOpenWindow == null)
            {
                yield break;
            }

            yield return StartCoroutine(_currentOpenWindow.OnBeforeCloseWindowEnumerator());
            yield return StartCoroutine(_currentOpenWindow.OnCloseWindowEnumerator());
            yield return StartCoroutine(_currentOpenWindow.OnAfterCloseWindowEnumerator());
            DestroyImmediate(_currentWindowGameObject);
            _currentWindowGameObject = null;
            _currentOpenWindow = null;
        }

        /// <summary>
        /// Screen を閉じる
        /// </summary>
        public IEnumerator OnCloseScreenEnumerator()
        {
            if (_currentOpenWindow == null)
            {
                yield break;
            }

            if (_currentOpenWindow.CurrentOpenScreen == null)
            {
                yield break;
            }

            yield return StartCoroutine(_currentOpenWindow.OnCloseScreenEnumerator());
            DestroyImmediate(_currentOpenWindow.CurrentOpenScreenPrefab);
            _currentOpenWindow.SetNullScreenPrefab();
        }
    }
}
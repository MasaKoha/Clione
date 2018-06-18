using System.Collections;
using UnityEngine;

namespace Clione.Home
{
    /// <summary>
    /// Scene の Base クラス
    /// </summary>
    public abstract class SceneBase : MonoBehaviour
    {
        /// <summary>
        /// Window を生成するルートオブジェクトの Transform
        /// </summary>
        [SerializeField] private Transform _windowRootTransform;

        /// <summary>
        /// 現在開かれている Window
        /// </summary>
        private WindowBase _currentOpenWindow;

        public string CurrentOpenWindowPath => _currentOpenWindow?.WindowPath ?? string.Empty;

        public string CurrentOpenScreenPath => _currentOpenWindow?.CurrentOpenScreenPath ?? string.Empty;

        /// <summary>
        /// 現在開かれている Window の GameObject
        /// </summary>
        private GameObject _currentWindowGameObject;

        /// <summary>
        /// 非同期の初期化処理
        /// </summary>
        public virtual IEnumerator Initialize(object param)
        {
            yield break;
        }

        /// <summary>
        /// Window を開く
        /// </summary>
        public IEnumerator OnOpenWindow(string nextWindowPath, string nextScreenPath,
            string currentWindowPath, string currentScreenPath)
        {
            if (currentWindowPath != nextWindowPath)
            {
                var prefab = Resources.Load<GameObject>(nextWindowPath);
                _currentWindowGameObject = Instantiate(prefab);
                _currentWindowGameObject.transform.SetParent(_windowRootTransform.transform, false);
                _currentOpenWindow = _currentWindowGameObject.GetComponent<WindowBase>();
                _currentOpenWindow.SetWindowPath(nextWindowPath);
                yield return StartCoroutine(_currentOpenWindow.Initialize());
            }

            yield return StartCoroutine(_currentOpenWindow.OnBeforeOpenWindow());
            yield return StartCoroutine(_currentOpenWindow.OnOpenWindow(nextScreenPath, currentScreenPath));
            yield return StartCoroutine(_currentOpenWindow.OnAfterOpenWindow());
        }

        /// <summary>
        /// Windowを閉じる
        /// </summary>
        public IEnumerator OnCloseWindow()
        {
            if (_currentOpenWindow == null)
            {
                yield break;
            }

            yield return StartCoroutine(_currentOpenWindow.OnBeforeCloseWindow());
            yield return StartCoroutine(_currentOpenWindow.OnCloseWindow());
            yield return StartCoroutine(_currentOpenWindow.OnAfterCloseWindow());
            DestroyImmediate(_currentWindowGameObject);
            _currentWindowGameObject = null;
            _currentOpenWindow = null;
        }

        /// <summary>
        /// Screen を閉じる
        /// </summary>
        public IEnumerator OnCloseScreen()
        {
            if (_currentOpenWindow == null)
            {
                yield break;
            }

            if (_currentOpenWindow.CurrentOpenScreen == null)
            {
                yield break;
            }

            yield return StartCoroutine(_currentOpenWindow.OnCloseScreen());
            DestroyImmediate(_currentOpenWindow.CurrentOpenScreenPrefab);
            _currentOpenWindow.SetNullScreenPrefab();
        }
    }
}
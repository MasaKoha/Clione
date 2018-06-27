using System.Collections;
using System.Collections.Generic;
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
        private readonly Dictionary<string, GameObject> _windowGameObjectList = new Dictionary<string, GameObject>();

        /// <summary>
        /// 非同期の初期化処理
        /// </summary>
        public virtual IEnumerator InitializeEnumerator(object param)
        {
            yield break;
        }

        /// <summary>
        /// Window を開く
        /// </summary>
        public IEnumerator OnOpenWindowEnumerator(string nextWindowPath, string nextScreenPath,
            string currentWindowPath, string currentScreenPath)
        {
            if (currentWindowPath != nextWindowPath)
            {
                if (!_windowGameObjectList.ContainsKey(nextWindowPath) || _windowGameObjectList[nextWindowPath] == null)
                {
                    var prefab = Resources.Load<GameObject>(nextWindowPath);
                    _windowGameObjectList.Add(nextWindowPath, Instantiate(prefab));
                    _windowGameObjectList[nextWindowPath].transform.SetParent(_windowRootTransform.transform, false);
                }

                _currentOpenWindow = _windowGameObjectList[nextWindowPath].GetComponent<WindowBase>();
                _currentOpenWindow.gameObject.SetActive(true);
                _currentOpenWindow.SetWindowPath(nextWindowPath);
                yield return StartCoroutine(_currentOpenWindow.InitializeEnumerator());
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
            _currentOpenWindow.gameObject.SetActive(false);
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
        }
    }
}
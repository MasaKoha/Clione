using System.Collections;
using System.Collections.Generic;
using Clione.ResourceLoader;
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
        /// これまで開いた Window の Base クラス
        /// </summary>
        private readonly Dictionary<string, WindowBase> _windowBaseList = new Dictionary<string, WindowBase>();

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
                if (!_windowBaseList.ContainsKey(nextWindowPath) || _windowBaseList[nextWindowPath] == null)
                {
                    var loaded = false;
                    GameObject prefab = null;
                    ClioneResourceLoader.LoadAsync<GameObject>(nextWindowPath,
                        uiParts =>
                        {
                            prefab = uiParts;
                            loaded = true;
                        },
                        () =>
                        {
                            Debug.LogError($"{nextWindowPath} is not found.");
                            loaded = true;
                        });

                    while (!loaded)
                    {
                        yield return null;
                    }

                    _windowBaseList.Add(nextWindowPath, Instantiate(prefab, _windowRootTransform).GetComponent<WindowBase>());
                    var initialize = _windowBaseList[nextWindowPath].InitializeEnumerator();
                    while (initialize.MoveNext())
                    {
                        yield return null;
                    }
                }

                _currentOpenWindow = _windowBaseList[nextWindowPath].GetComponent<WindowBase>();
                _currentOpenWindow.gameObject.SetActive(true);
                _currentOpenWindow.SetWindowPath(nextWindowPath);
            }

            var onBeforeOpen = _currentOpenWindow.OnBeforeOpenWindowEnumerator();
            while (onBeforeOpen.MoveNext())
            {
                yield return null;
            }

            var onOpen = _currentOpenWindow.OnOpenWindowEnumerator(nextScreenPath, currentScreenPath);
            while (onOpen.MoveNext())
            {
                yield return null;
            }

            var onAfterOpen = _currentOpenWindow.OnAfterOpenWindowEnumerator();
            while (onAfterOpen.MoveNext())
            {
                yield return null;
            }
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

            var onBeforeClose = _currentOpenWindow.OnBeforeCloseWindowEnumerator();
            while (onBeforeClose.MoveNext())
            {
                yield return null;
            }

            var onClose = _currentOpenWindow.OnCloseWindowEnumerator();
            while (onClose.MoveNext())
            {
                yield return null;
            }

            var onAfterClose = _currentOpenWindow.OnAfterCloseWindowEnumerator();
            while (onAfterClose.MoveNext())
            {
                yield return null;
            }

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

            var onClose = _currentOpenWindow.OnCloseScreenEnumerator();
            while (onClose.MoveNext())
            {
                yield return null;
            }
        }
    }
}
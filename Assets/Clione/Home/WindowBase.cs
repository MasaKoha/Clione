using System.Collections;
using System.Collections.Generic;
using Clione.ResourceLoader;
using UnityEngine;

namespace Clione.Home
{
    /// <summary>
    /// Window のベースクラス
    /// </summary>
    public abstract class WindowBase : MonoBehaviour
    {
        /// <summary>
        /// 現在開かれている Screen
        /// </summary>
        public ScreenBase CurrentOpenScreen { get; private set; }

        public string CurrentOpenScreenPath => CurrentOpenScreen?.ScreenPath ?? string.Empty;

        public string WindowPath { get; private set; }

        /// <summary>
        /// これまで開いた Screen の Base クラス
        /// </summary>
        public Dictionary<string, ScreenBase> ScreenBaseList = new Dictionary<string, ScreenBase>();

        public void SetWindowPath(string path) => WindowPath = path;

        /// <summary>
        /// 初期化
        /// </summary>
        public virtual IEnumerator InitializeEnumerator()
        {
            yield break;
        }

        #region Window Open

        /// <summary>
        /// Window が開かれる前の処理
        /// </summary>
        public virtual IEnumerator OnBeforeOpenWindowEnumerator()
        {
            yield break;
        }

        /// <summary>
        /// Window が開かられるときの処理
        /// </summary>
        public IEnumerator OnOpenWindowEnumerator(string nextScreenPath, string currentScreenPath)
        {
            if (currentScreenPath != nextScreenPath)
            {
                if (!ScreenBaseList.ContainsKey(nextScreenPath) || ScreenBaseList[nextScreenPath] == null)
                {
                    var loaded = false;
                    GameObject prefab = null;
                    ClioneResourceLoader.LoadAsync<GameObject>(nextScreenPath,
                        uiParts =>
                        {
                            prefab = uiParts;
                            loaded = true;
                        },
                        () =>
                        {
                            Debug.LogError($"{nextScreenPath} is not found.");
                            loaded = true;
                        });

                    while (!loaded)
                    {
                        yield return null;
                    }

                    ScreenBaseList.Add(nextScreenPath, Instantiate(prefab, this.transform).GetComponent<ScreenBase>());
                    var initialize = ScreenBaseList[nextScreenPath].InitializeEnumerator();
                    while (initialize.MoveNext())
                    {
                        yield return null;
                    }
                }

                CurrentOpenScreen = ScreenBaseList[nextScreenPath];
                CurrentOpenScreen.gameObject.SetActive(true);
                CurrentOpenScreen.SetScreenPath(nextScreenPath);
            }


            var onBeforeOpen = CurrentOpenScreen.OnBeforeOpenScreenEnumerator();
            while (onBeforeOpen.MoveNext())
            {
                yield return null;
            }

            var onOpen = CurrentOpenScreen.OnOpenScreenEnumerator();
            while (onOpen.MoveNext())
            {
                yield return null;
            }

            var onAfterOpen = CurrentOpenScreen.OnAfterOpenScreenEnumerator();
            while (onAfterOpen.MoveNext())
            {
                yield return null;
            }
        }

        /// <summary>
        /// Window が開かれたあとの処理
        /// </summary>
        public virtual IEnumerator OnAfterOpenWindowEnumerator()
        {
            yield break;
        }

        #endregion

        #region Window Close

        /// <summary>
        /// Window を閉じる前の処理
        /// </summary>
        public virtual IEnumerator OnBeforeCloseWindowEnumerator()
        {
            yield break;
        }

        /// <summary>
        /// Window を閉じるときの処理
        /// </summary>
        public IEnumerator OnCloseWindowEnumerator()
        {
            if (CurrentOpenScreen == null)
            {
                yield break;
            }

            var onClose = OnCloseScreenEnumerator();
            while (onClose.MoveNext())
            {
                yield return null;
            }
        }

        /// <summary>
        /// Window を閉じたあとの処理
        /// </summary>
        public virtual IEnumerator OnAfterCloseWindowEnumerator()
        {
            yield break;
        }

        #endregion

        /// <summary>
        /// 現在開かれている Screen が閉じられる前の処理
        /// </summary>
        public IEnumerator OnCloseScreenEnumerator()
        {
            var onBeforeClose = CurrentOpenScreen.OnBeforeCloseScreenEnumerator();
            while (onBeforeClose.MoveNext())
            {
                yield return null;
            }

            var onClose = CurrentOpenScreen.OnCloseScreenEnumerator();
            while (onClose.MoveNext())
            {
                yield return null;
            }

            var onAfterClose = CurrentOpenScreen.OnAfterCloseScreenEnumerator();
            while (onAfterClose.MoveNext())
            {
                yield return null;
            }

            CurrentOpenScreen.gameObject.SetActive(false);
        }
    }
}
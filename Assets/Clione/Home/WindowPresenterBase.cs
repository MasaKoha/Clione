using System;
using System.Collections;
using UnityEngine;

namespace Clione
{
    /// <summary>
    /// WindowPresenter のベースクラス
    /// </summary>
    public abstract class WindowPresenterBase : MonoBehaviour
    {
        /// <summary>
        /// 現在開かれている Screen
        /// </summary>
        public ScreenPresenterBase CurrentOpenScreen { get; private set; }

        public string CurrentOpenScreenPath => CurrentOpenScreen?.ScreenPath ?? string.Empty;

        public string WindowPath { get; private set; }

        /// <summary>
        /// 現在開かれている Screen の GameObject
        /// </summary>
        public GameObject CurrentOpenScreenPrefab { get; private set; }

        public void SetNullScreenPrefab()
        {
            CurrentOpenScreenPrefab = null;
        }

        public void SetWindowPath(string path) => WindowPath = path;

        /// <summary>
        /// 初期化
        /// </summary>
        public virtual void Initialize() => StartCoroutine(InitializeEnumerator());

        /// <summary>
        /// 非同期用初期化
        /// </summary>
        protected virtual IEnumerator InitializeEnumerator()
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
            if (CurrentOpenScreenPrefab == null)
            {
                if (currentScreenPath != nextScreenPath)
                {
                    var prefab = Resources.Load<GameObject>(nextScreenPath);
                    CurrentOpenScreenPrefab = Instantiate(prefab, this.transform);
                    CurrentOpenScreen = CurrentOpenScreenPrefab.GetComponent<ScreenPresenterBase>();
                    CurrentOpenScreen.SetScreenPath(nextScreenPath);
                    CurrentOpenScreen.Initialize();
                }
            }

            yield return StartCoroutine(CurrentOpenScreen.OnBeforeOpenScreenEnumerator());
            yield return StartCoroutine(CurrentOpenScreen.OnOpenScreenEnumerator());
            yield return StartCoroutine(CurrentOpenScreen.OnAfterOpenScreenEnumerator());
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

            yield return StartCoroutine(OnCloseScreenEnumerator());
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
            yield return StartCoroutine(CurrentOpenScreen.OnBeforeCloseScreenEnumerator());
            yield return StartCoroutine(CurrentOpenScreen.OnCloseScreenEnumerator());
            yield return StartCoroutine(CurrentOpenScreen.OnAfterCloseScreenEnumerator());
            DestroyImmediate(CurrentOpenScreenPrefab);
            CurrentOpenScreenPrefab = null;
        }
    }
}
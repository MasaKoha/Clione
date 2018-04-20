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

        /// <summary>
        /// 現在開かれている Screen の GameObject
        /// </summary>
        public GameObject CurrentOpenScreenPrefab { get; private set; }

        /// <summary>
        /// Screen が開きはじめたときのイベント
        /// </summary>
        public event Action BeginOpenScreenAction;

        /// <summary>
        /// Screen が開き終わったときのイベント
        /// </summary>
        public event Action EndOpenScreenAction;

        /// <summary>
        /// Screen が閉じ終わる前のイベント
        /// </summary>
        public event Action BeginCloseScreenAction;

        /// <summary>
        /// Screen が閉じ終わったときのイベント
        /// </summary>
        public event Action EndCloseScreenAction;

        /// <summary>
        /// 初期化
        /// </summary>
        public virtual void Initialize()
        {
            StartCoroutine(InitializeEnumerator());
        }

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
        public IEnumerator OnOpenWindowEnumerator(string screenPrefabPath, string currentScreenPath)
        {
            BeginOpenScreenAction?.Invoke();
            if (CurrentOpenScreenPrefab == null)
            {
                if (currentScreenPath != screenPrefabPath)
                {
                    var prefab = Resources.Load<GameObject>(screenPrefabPath);
                    CurrentOpenScreenPrefab = Instantiate(prefab, this.transform);
                    CurrentOpenScreen = CurrentOpenScreenPrefab.GetComponent<ScreenPresenterBase>();
                    CurrentOpenScreen.Initialize();
                }
            }

            yield return StartCoroutine(CurrentOpenScreen.OnBeforeOpenScreenEnumerator());
            yield return StartCoroutine(CurrentOpenScreen.OnOpenScreenEnumerator());
            yield return StartCoroutine(CurrentOpenScreen.OnAfterOpenScreenEnumerator());
            EndOpenScreenAction?.Invoke();
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
            BeginCloseScreenAction?.Invoke();
            yield return StartCoroutine(CurrentOpenScreen.OnBeforeCloseScreenEnumerator());
            yield return StartCoroutine(CurrentOpenScreen.OnCloseScreenEnumerator());
            yield return StartCoroutine(CurrentOpenScreen.OnAfterCloseScreenEnumerator());
            DestroyImmediate(CurrentOpenScreenPrefab);
            CurrentOpenScreenPrefab = null;
            EndCloseScreenAction?.Invoke();
        }
    }
}
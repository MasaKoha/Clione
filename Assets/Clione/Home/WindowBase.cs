using System.Collections;
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
        public virtual IEnumerator Initialize()
        {
            yield break;
        }

        #region Window Open

        /// <summary>
        /// Window が開かれる前の処理
        /// </summary>
        public virtual IEnumerator OnBeforeOpenWindow()
        {
            yield break;
        }

        /// <summary>
        /// Window が開かられるときの処理
        /// </summary>
        public IEnumerator OnOpenWindow(string nextScreenPath, string currentScreenPath)
        {
            if (CurrentOpenScreenPrefab == null)
            {
                if (currentScreenPath != nextScreenPath)
                {
                    var prefab = Resources.Load<GameObject>(nextScreenPath);
                    CurrentOpenScreenPrefab = Instantiate(prefab, this.transform);
                    CurrentOpenScreen = CurrentOpenScreenPrefab.GetComponent<ScreenBase>();
                    CurrentOpenScreen.SetScreenPath(nextScreenPath);
                    yield return StartCoroutine(CurrentOpenScreen.Initialize());
                }
            }

            yield return StartCoroutine(CurrentOpenScreen.OnBeforeOpenScreen());
            yield return StartCoroutine(CurrentOpenScreen.OnOpenScreen());
            yield return StartCoroutine(CurrentOpenScreen.OnAfterOpenScreen());
        }

        /// <summary>
        /// Window が開かれたあとの処理
        /// </summary>
        public virtual IEnumerator OnAfterOpenWindow()
        {
            yield break;
        }

        #endregion

        #region Window Close

        /// <summary>
        /// Window を閉じる前の処理
        /// </summary>
        public virtual IEnumerator OnBeforeCloseWindow()
        {
            yield break;
        }

        /// <summary>
        /// Window を閉じるときの処理
        /// </summary>
        public IEnumerator OnCloseWindow()
        {
            if (CurrentOpenScreen == null)
            {
                yield break;
            }

            yield return StartCoroutine(OnCloseScreen());
        }

        /// <summary>
        /// Window を閉じたあとの処理
        /// </summary>
        public virtual IEnumerator OnAfterCloseWindow()
        {
            yield break;
        }

        #endregion

        /// <summary>
        /// 現在開かれている Screen が閉じられる前の処理
        /// </summary>
        public IEnumerator OnCloseScreen()
        {
            yield return StartCoroutine(CurrentOpenScreen.OnBeforeCloseScreen());
            yield return StartCoroutine(CurrentOpenScreen.OnCloseScreen());
            yield return StartCoroutine(CurrentOpenScreen.OnAfterCloseScreen());
            DestroyImmediate(CurrentOpenScreenPrefab);
            CurrentOpenScreenPrefab = null;
        }
    }
}
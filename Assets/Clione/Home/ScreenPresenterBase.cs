using System.Collections;
using UnityEngine;

namespace Clione.Home
{
    /// <summary>
    /// ScreenPresenter のベースクラス
    /// </summary>
    public abstract class ScreenPresenterBase : MonoBehaviour
    {
        public string ScreenPath { get; private set; }

        public void SetScreenPath(string path) => ScreenPath = path;

        /// <summary>
        /// 初期化
        /// </summary>
        public virtual void Initialize() => StartCoroutine(InitializeEnumerator());

        /// <summary>
        /// 非同期用の初期化
        /// </summary>
        protected virtual IEnumerator InitializeEnumerator()
        {
            yield break;
        }

        #region Screen Open

        /// <summary>
        /// Screen を開く前の処理
        /// </summary>
        public virtual IEnumerator OnBeforeOpenScreenEnumerator()
        {
            yield break;
        }

        /// <summary>
        /// Screen を開くときの処理
        /// </summary>
        public virtual IEnumerator OnOpenScreenEnumerator()
        {
            yield break;
        }

        /// <summary>
        /// Screen が開かれたあとの処理
        /// </summary>
        public virtual IEnumerator OnAfterOpenScreenEnumerator()
        {
            yield break;
        }

        #endregion

        #region Screen Close

        /// <summary>
        /// Screen を閉じる前の処理
        /// </summary>
        public virtual IEnumerator OnBeforeCloseScreenEnumerator()
        {
            yield break;
        }

        /// <summary>
        /// Screen を閉じたときの処理
        /// </summary>
        public virtual IEnumerator OnCloseScreenEnumerator()
        {
            yield break;
        }

        /// <summary>
        /// Screen を閉じたあとの処理
        /// </summary>
        public virtual IEnumerator OnAfterCloseScreenEnumerator()
        {
            yield break;
        }

        #endregion
    }
}
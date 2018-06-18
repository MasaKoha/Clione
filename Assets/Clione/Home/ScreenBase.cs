using System.Collections;
using UnityEngine;

namespace Clione.Home
{
    /// <summary>
    /// Screen のベースクラス
    /// </summary>
    public abstract class ScreenBase : MonoBehaviour
    {
        public string ScreenPath { get; private set; }

        public void SetScreenPath(string path) => ScreenPath = path;

        /// <summary>
        /// 初期化
        /// </summary>
        public virtual IEnumerator Initialize()
        {
            yield break;
        }

        #region Screen Open

        /// <summary>
        /// Screen を開く前の処理
        /// </summary>
        public virtual IEnumerator OnBeforeOpenScreen()
        {
            yield break;
        }

        /// <summary>
        /// Screen を開くときの処理
        /// </summary>
        public virtual IEnumerator OnOpenScreen()
        {
            yield break;
        }

        /// <summary>
        /// Screen が開かれたあとの処理
        /// </summary>
        public virtual IEnumerator OnAfterOpenScreen()
        {
            yield break;
        }

        #endregion

        #region Screen Close

        /// <summary>
        /// Screen を閉じる前の処理
        /// </summary>
        public virtual IEnumerator OnBeforeCloseScreen()
        {
            yield break;
        }

        /// <summary>
        /// Screen を閉じたときの処理
        /// </summary>
        public virtual IEnumerator OnCloseScreen()
        {
            yield break;
        }

        /// <summary>
        /// Screen を閉じたあとの処理
        /// </summary>
        public virtual IEnumerator OnAfterCloseScreen()
        {
            yield break;
        }

        #endregion
    }
}
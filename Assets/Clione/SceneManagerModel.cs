namespace Clione
{
    /// <summary>
    /// SceneManager に関する Model クラス
    /// </summary>
    public class SceneManagerModel
    {
        /// <summary>
        /// 現在のシーン名
        /// </summary>
        public string CurrentSceneName => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;

        /// <summary>
        /// 現在開かれている Window のパス
        /// </summary>
        public string CurrentWindowPath { get; private set; }

        /// <summary>
        /// 現在開かれている Screen のパス
        /// </summary>
        public string CurrentScreenPath { get; private set; }

        /// <summary>
        /// Window のパスをセットする
        /// </summary>
        public void SetCurrentWindowPath(string windowPath)
        {
            CurrentWindowPath = windowPath;
        }

        /// <summary>
        /// Screen のパスをセットする
        /// </summary>
        public void SetCurrentScreenPath(string screenPath)
        {
            CurrentScreenPath = screenPath;
        }
    }
}
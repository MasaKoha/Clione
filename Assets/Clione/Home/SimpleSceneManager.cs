using System;
using System.Collections;

namespace Clione.Home
{
    /// <summary>
    /// Simple Scene Manager
    /// This class is used by only LoadScene.
    /// </summary>
    public class SimpleSceneManager : ClioneSceneManager
    {
        public override IEnumerator LoadWindow(string loadWindowPath, string loadScreenPath, Action onComplete = null)
        {
            throw new NotImplementedException("Not Implement LoadWindow.");
        }

        public override IEnumerator LoadScreen(string loadScreenPath, Action onComplete = null)
        {
            throw new NotImplementedException("Not Implement LoadScreen.");
        }
    }
}
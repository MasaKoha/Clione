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
        public override IEnumerator LoadWindowEnumerator(string loadWindowPath, string loadScreenPath, Action onComplete = null)
        {
            throw new NotImplementedException("Not Implement LoadWindow.");
        }

        public override IEnumerator LoadScreenEnumerator(string loadScreenPath, Action onComplete = null)
        {
            throw new NotImplementedException("Not Implement LoadScreen.");
        }
    }
}
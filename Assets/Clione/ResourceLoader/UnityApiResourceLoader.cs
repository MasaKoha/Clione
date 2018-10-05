using System;
using System.Collections;
using Clione.Core;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Clione.ResourceLoader
{
    public class UnityApiResourceLoader : IResourceLoader
    {
        public void LoadAsync<T>(string filePath, Action<T> onComplete, Action onFail)
            where T : Object
        {
            ClioneCore.Run(LoadAsyncEnumerator(filePath, onComplete, onFail));
        }

        private IEnumerator LoadAsyncEnumerator<T>(string filePath, Action<T> onComplete, Action onFail)
            where T : Object
        {
            var resource = Resources.LoadAsync<T>(filePath);
            while (!resource.isDone)
            {
                yield return null;
            }

            if (resource.asset == null)
            {
                onFail.Invoke();
                yield break;
            }

            onComplete.Invoke(resource.asset as T);
        }
    }
}
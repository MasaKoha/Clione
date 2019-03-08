using System;
using Object = UnityEngine.Object;

namespace Clione.ResourceLoader
{
    public static class ClioneResourceLoader
    {
        private static IResourceLoader _loader;

        public static void Initialize(IResourceLoader loader)
        {
            _loader = loader;
        }

        public static void LoadAsync<T>(string filePath, Action<T> onComplete, Action onFail)
            where T : Object
        {
            _loader.LoadAsync(filePath, onComplete, onFail);
        }
    }
}
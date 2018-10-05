using System;
using Object = UnityEngine.Object;

namespace Clione.ResourceLoader
{
    public interface IResourceLoader
    {
        void LoadAsync<T>(string filePath, Action<T> onComplete, Action onFail) where T : Object;
    }
}
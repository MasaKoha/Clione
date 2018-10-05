using System.Collections;
using Clione.ResourceLoader;
using Clione.Utility;
using UnityEngine;

namespace Clione.Core
{
    public class ClioneCore
    {
        private const string GameObjectName = "[Clione MainThread Dispacher]";

        private readonly ClioneDispatcher _clioneDispatcher;

        private static ClioneCore _clioneCore = null;

        public static void Initialize(IResourceLoader loader)
        {
            if (_clioneCore != null) return;
            _clioneCore = new ClioneCore(loader);
        }

        private ClioneCore(IResourceLoader loader)
        {
            var gameObject = new GameObject {name = GameObjectName};
            Object.DontDestroyOnLoad(gameObject);
            _clioneDispatcher = gameObject.AddComponent<ClioneDispatcher>();
            ClioneResourceLoader.Initialize(loader);
        }

        public static void Run(IEnumerator coroutine)
        {
            _clioneCore._clioneDispatcher.Run(coroutine);
        }
    }
}
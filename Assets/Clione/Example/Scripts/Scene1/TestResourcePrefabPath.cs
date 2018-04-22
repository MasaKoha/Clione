using UnityEngine;

namespace Clione.Example
{
    public static class TestResourcePrefabPath
    {
        private const string WindowPath = "Window/";
        private const string ScreenPath = "Screen/";

        public static string GetWindowPath(string windowName) => $"{WindowPath}{windowName}";
        public static string GetScreenPath(string screenPath) => $"{ScreenPath}{screenPath}";
    }
}
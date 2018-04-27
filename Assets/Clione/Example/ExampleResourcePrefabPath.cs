namespace Clione.Example
{
    public static class ExampleResourcePrefabPath
    {
        private const string WindowPath = "Window/";
        private const string ScreenPath = "Screen/";
        private const string DrillDownViewerPath = "DrillDownViewer/";

        public static string GetWindowPath(string windowName) => $"{WindowPath}{windowName}";
        public static string GetScreenPath(string screenPath) => $"{ScreenPath}{screenPath}";

        public static string GetDrillDownViewerPath(string drillDownViewPath) =>
            $"{DrillDownViewerPath}{drillDownViewPath}";
    }
}
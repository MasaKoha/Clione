using Clione.Home;

namespace Clione.Example
{
    public class OutGameScenePresenter : ScenePresenterBase
    {
        public override void Initialize(object param)
        {
        }

        public override void InitializeOpenWindowAndScreen()
        {
            OpenWindow(OutGameWindowType.HomeWindow, OutGameScreenType.HomeScreen);
        }

        private static void OpenWindow(OutGameWindowType windowType, OutGameScreenType screenType)
        {
            string windowPath = ExampleResourcePrefabPath.GetWindowPath(windowType.ToString());
            string screenPath = ExampleResourcePrefabPath.GetScreenPath(screenType.ToString());
            SceneLoader.LoadWindow(windowPath, screenPath);
        }
    }
}
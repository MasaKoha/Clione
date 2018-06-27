using System.Collections;
using Clione.Home;

namespace Clione.Example
{
    public class OutGameScene : SceneBase
    {
        protected override IEnumerator OnInitialize(object param)
        {
            var windowPath = ExampleResourcePrefabPath.GetWindowPath(OutGameWindowType.HomeWindow.ToString());
            var screenPath = ExampleResourcePrefabPath.GetScreenPath(OutGameScreenType.HomeScreen.ToString());
            SceneLoader.LoadWindow(windowPath, screenPath);
            yield break;
        }
    }
}
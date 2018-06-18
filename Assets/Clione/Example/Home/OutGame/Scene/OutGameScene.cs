using System.Collections;
using Clione.Home;

namespace Clione.Example
{
    public class OutGameScene : SceneBase
    {
        public override IEnumerator Initialize(object param)
        {
            var windowPath = ExampleResourcePrefabPath.GetWindowPath(OutGameWindowType.HomeWindow.ToString());
            var screenPath = ExampleResourcePrefabPath.GetScreenPath(OutGameScreenType.HomeScreen.ToString());
            SceneLoader.LoadWindow(windowPath, screenPath);
            yield break;
        }
    }
}
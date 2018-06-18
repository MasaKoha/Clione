using System.Collections;
using Clione.Home;

namespace Clione.Example
{
    public class HomeExampleMainScene : SceneBase
    {
        private void Start()
        {
            SceneLoader.Initialize(new ClioneSceneManager());
            SceneLoader.LoadScene("OutGame");
        }
    }
}
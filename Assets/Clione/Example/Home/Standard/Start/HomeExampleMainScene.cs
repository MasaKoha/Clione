using System.Collections;
using Clione.Home;

namespace Clione.Example
{
    public class HomeExampleMainScene : SceneBase
    {
        private void Start()
        {
            SceneLoader.Initialize(new ClioneSceneManager());
        }

        protected override IEnumerator OnInitialize(object param)
        {
            SceneLoader.LoadScene("OutGame");
            yield break;
        }
    }
}
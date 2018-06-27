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

        public override IEnumerator InitializeEnumerator(object param)
        {
            SceneLoader.LoadScene("OutGame");
            yield break;
        }
    }
}
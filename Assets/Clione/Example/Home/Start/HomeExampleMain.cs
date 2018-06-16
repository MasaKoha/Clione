using UnityEngine;

namespace Clione.Example
{
    public class HomeExampleMain : ScenePresenterBase
    {
        private void Start()
        {
            SceneLoader.Instance.Initialize();
        }

        public override void Initialize(object param)
        {
        }

        public override void InitializeOpenWindowAndScreen()
        {
            SceneLoader.Instance.LoadScene("OutGame");
        }
    }
}
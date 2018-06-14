using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public class StartSceneMain : MonoBehaviour
    {
        private void Start()
        {
            SceneLoader.Initialize(new ClioneSceneManager());
            SceneLoader.LoadScene("OutGame");
        }
    }
}
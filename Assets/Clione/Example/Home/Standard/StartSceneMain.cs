using Clione.Core;
using Clione.Home;
using Clione.ResourceLoader;
using UnityEngine;

namespace Clione.Example
{
    public class StartSceneMain : MonoBehaviour
    {
        private void Start()
        {
            ClioneCore.Initialize(new UnityApiResourceLoader());
            SceneLoader.Initialize(new ClioneSceneManager());
            SceneLoader.LoadScene("OutGame");
        }
    }
}
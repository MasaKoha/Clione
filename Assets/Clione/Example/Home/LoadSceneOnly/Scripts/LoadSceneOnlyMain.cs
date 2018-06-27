using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public class LoadSceneOnlyMain : MonoBehaviour
    {
        private void Start()
        {
            SceneLoader.Initialize(new SimpleSceneManager());
            SceneLoader.LoadScene("Scene1");
        }
    }
}
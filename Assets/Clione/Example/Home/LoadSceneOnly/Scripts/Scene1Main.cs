using System.Collections;
using Clione.Home;
using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class Scene1Main : SceneBase
    {
        [SerializeField] private Button _loadSceneButton;

        public override IEnumerator InitializeEnumerator(object param)
        {
            SetEvent();
            yield break;
        }

        private void SetEvent()
        {
            var sceneParam = new SceneParam("SceneTransitionTest", 100);
            Debug.Log("In Scene1");
            Debug.Log($"SceneParam Text  : {sceneParam.Text}");
            Debug.Log($"SceneParam Value : {sceneParam.Value}");
            _loadSceneButton.onClick.AddListener(() => SceneLoader.LoadScene("Scene2", sceneParam));
        }
    }
}
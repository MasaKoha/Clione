using System.Collections;
using Clione.Example;
using UnityEngine;
using UnityEngine.Assertions;

namespace Clione.Home
{
    public class Scene2Main : SceneBase
    {
        public override IEnumerator InitializeEnumerator(object param)
        {
            var parameter = param as SceneParam;
            Assert.IsNotNull(parameter);
            Debug.Log("In Scene2");
            Debug.Log($"SceneParam Text  : {parameter.Text}");
            Debug.Log($"SceneParam Value : {parameter.Value}");
            yield break;
        }
    }
}
using System.Collections;
using Clione.Home;
using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class InGameScene : SceneBase
    {
        [SerializeField] private Button _outGameButton;

        public override IEnumerator Initialize(object param)
        {
            _outGameButton.onClick.AddListener(() => SceneLoader.LoadScene("OutGame"));
            yield break;
        }
    }
}
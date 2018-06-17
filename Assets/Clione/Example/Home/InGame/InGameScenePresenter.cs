using Clione.Home;
using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class InGameScenePresenter : ScenePresenterBase
    {
        [SerializeField] private Button _outGameButton;

        public override void Initialize(object param)
        {
            _outGameButton.onClick.AddListener(() => SceneLoader.LoadScene("OutGame"));
        }

        public override void InitializeOpenWindowAndScreen()
        {
            // Screen や Window を使わないならここに書かない
        }
    }
}
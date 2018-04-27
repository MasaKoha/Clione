using System.Collections;
using UnityEngine;

namespace Clione.Example
{
    public class Scene1ScreenBasePresenter : ScreenPresenterBase
    {
        [SerializeField] private Scene1ScreenBaseView _view;

        public override void Initialize()
        {
            _view.Initialize();
        }

        public override IEnumerator OnBeforeOpenScreenEnumerator()
        {
            yield break;
        }
    }
}
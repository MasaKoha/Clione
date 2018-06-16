using System.Collections;
using UnityEngine;

namespace Clione.Example
{
    public class HomeScreenPresenter : ScreenPresenterBase
    {
        [SerializeField] private HomeScreenView _view;

        public override void Initialize()
        {
            _view.Initialize();
        }

        public override IEnumerator OnBeforeOpenScreenEnumerator()
        {
            yield break;
        }

        public override IEnumerator OnBeforeCloseScreenEnumerator()
        {
            yield break;
        }
    }
}
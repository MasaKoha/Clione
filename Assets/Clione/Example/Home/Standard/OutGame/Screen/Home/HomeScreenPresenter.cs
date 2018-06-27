using System.Collections;
using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public class HomeScreenPresenter : ScreenBase
    {
        [SerializeField] private HomeScreenView _view;

        public override IEnumerator InitializeEnumerator()
        {
            _view.Initialize();
            yield break;
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
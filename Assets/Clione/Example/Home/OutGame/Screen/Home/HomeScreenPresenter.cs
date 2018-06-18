using System.Collections;
using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public class HomeScreenPresenter : ScreenBase
    {
        [SerializeField] private HomeScreenView _view;

        public override IEnumerator Initialize()
        {
            _view.Initialize();
            yield break;
        }

        public override IEnumerator OnBeforeOpenScreen()
        {
            yield break;
        }

        public override IEnumerator OnBeforeCloseScreen()
        {
            yield break;
        }
    }
}
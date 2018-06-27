using System.Collections;
using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public class DomesticScreenPresenter : ScreenBase
    {
        [SerializeField] private DomesticScreenView _view;

        public override IEnumerator Initialize()
        {
            _view.Initialize();
            SetEvent();
            yield break;
        }

        private void SetEvent()
        {
        }
    }
}
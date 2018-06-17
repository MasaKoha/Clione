using UnityEngine;

namespace Clione.Example
{
    public class DomesticScreenPresenter : ScreenPresenterBase
    {
        [SerializeField] private DomesticScreenView _view;

        public override void Initialize()
        {
            _view.Initialize();
            SetEvent();
        }

        private void SetEvent()
        {
        }
    }
}
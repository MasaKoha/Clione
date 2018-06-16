using UnityEngine;

namespace Clione.Example
{
    public class ShopScreenPresenter : ScreenPresenterBase
    {
        [SerializeField] private ShopScreenView _view;

        public override void Initialize()
        {
            _view.Initialize();
        }
    }
}
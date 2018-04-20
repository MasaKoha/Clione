using UnityEngine;

namespace Clione.Example
{
    public class Window2Presenter : WindowPresenterBase
    {
        [SerializeField] private Window2View _view;

        public override void Initialize()
        {
            _view.Initialize();
        }
    }
}
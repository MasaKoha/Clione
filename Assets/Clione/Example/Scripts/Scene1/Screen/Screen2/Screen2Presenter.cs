using UnityEngine;

namespace Clione.Example
{
    public class Screen2Presenter : ScreenPresenterBase
    {
        [SerializeField] private Screen2View _view;

        public override void Initialize()
        {
            _view.Initialize();
        }
    }
}
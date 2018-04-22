using UnityEngine;

namespace Clione.Example
{
    public class Screen4Presenter : ScreenPresenterBase
    {
        [SerializeField] private Screen4View _view;

        public override void Initialize()
        {
            _view.Initialize();
        }
    }
}
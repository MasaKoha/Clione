using UnityEngine;

namespace Clione.Example
{
    public class Screen3Presenter : ScreenPresenterBase
    {
        [SerializeField] private Screen3View _view;

        public override void Initialize()
        {
            _view.Initialize();
        }
    }
}
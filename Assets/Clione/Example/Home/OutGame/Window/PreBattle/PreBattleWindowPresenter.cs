using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public class PreBattleWindowPresenter : WindowPresenterBase
    {
        [SerializeField] private PreBattleWindowView _view;

        public override void Initialize()
        {
            _view.Initialize();
        }
    }
}
using System.Collections;
using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public class PreBattleWindowPresenter : WindowBase
    {
        [SerializeField] private PreBattleWindowView _view;

        public override IEnumerator Initialize()
        {
            _view.Initialize();
            yield break;
        }
    }
}
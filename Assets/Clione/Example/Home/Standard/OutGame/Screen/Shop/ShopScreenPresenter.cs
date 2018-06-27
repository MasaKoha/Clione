using System.Collections;
using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public class ShopScreenPresenter : ScreenBase
    {
        [SerializeField] private ShopScreenView _view;

        public override IEnumerator Initialize()
        {
            _view.Initialize();
            yield break;
        }
    }
}
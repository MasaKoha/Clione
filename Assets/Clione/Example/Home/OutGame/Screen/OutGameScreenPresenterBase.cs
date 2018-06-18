using System.Collections;
using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public abstract class OutGameScreenPresenterBase<T> : ScreenBase where T : OutGameScreenViewBase
    {
        [SerializeField] protected T View;

        public override IEnumerator Initialize()
        {
            View.Initialize();
            yield break;
        }

        public override IEnumerator OnBeforeOpenScreen()
        {
            yield break;
        }
    }
}
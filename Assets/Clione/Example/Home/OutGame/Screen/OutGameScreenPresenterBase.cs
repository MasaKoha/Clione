using System.Collections;
using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public abstract class OutGameScreenPresenterBase<T> : ScreenPresenterBase where T : OutGameScreenViewBase
    {
        [SerializeField] protected T View;

        public override void Initialize()
        {
            View.Initialize();
        }

        public override IEnumerator OnBeforeOpenScreenEnumerator()
        {
            yield break;
        }
    }
}
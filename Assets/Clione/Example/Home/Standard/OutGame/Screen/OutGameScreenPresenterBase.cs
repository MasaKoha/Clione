using System.Collections;
using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public abstract class OutGameScreenPresenterBase<T> : ScreenBase where T : OutGameScreenViewBase
    {
        [SerializeField] protected T View;

        public override IEnumerator InitializeEnumerator()
        {
            View.Initialize();
            yield break;
        }

        public override IEnumerator OnBeforeOpenScreenEnumerator()
        {
            yield break;
        }
    }
}
using System.Collections;
using Clione.Home;
using Clione.Utility;
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

        public override IEnumerator OnOpenScreenEnumerator()
        {
            var show = View.ShowEnumerator();
            while (show.MoveNext())
            {
                yield return null;
            }
        }

        public override IEnumerator OnCloseScreenEnumerator()
        {
            var hide = View.HideEnumerator();
            while (hide.MoveNext())
            {
                yield return null;
            }
        }
    }
}
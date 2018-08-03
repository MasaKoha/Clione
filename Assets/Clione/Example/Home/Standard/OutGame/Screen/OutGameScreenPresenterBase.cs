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
            yield return StartCoroutine(View.ShowEnumerator());
        }

        public override IEnumerator OnCloseScreenEnumerator()
        {
            yield return StartCoroutine(View.HideEnumerator());
        }
    }
}
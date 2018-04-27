using System.Collections;
using UnityEngine;

namespace Clione.Example
{
    public class Screen1Presenter : ScreenPresenterBase
    {
        [SerializeField] private Screen1View _view;

        public override void Initialize()
        {
            this.transform.localScale = new Vector3(0.7f, 0.7f, 1f);
            _view.Initialize();
        }

        public override IEnumerator OnBeforeOpenScreenEnumerator()
        {
            yield break;
        }

        public override IEnumerator OnBeforeCloseScreenEnumerator()
        {
            yield break;
        }
    }
}
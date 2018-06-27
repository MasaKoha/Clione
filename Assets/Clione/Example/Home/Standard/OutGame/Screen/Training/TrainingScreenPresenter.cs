using System.Collections;
using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public class TrainingScreenPresenter : ScreenBase
    {
        [SerializeField] private TrainingScreenView _view;

        public override IEnumerator InitializeEnumerator()
        {
            _view.Initialize();
            yield break;
        }
    }
}
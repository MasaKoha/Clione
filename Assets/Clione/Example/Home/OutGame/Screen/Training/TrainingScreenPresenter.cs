using UnityEngine;

namespace Clione.Example
{
    public class TrainingScreenPresenter : ScreenPresenterBase
    {
        [SerializeField] private TrainingScreenView _view;

        public override void Initialize()
        {
            _view.Initialize();
        }
    }
}
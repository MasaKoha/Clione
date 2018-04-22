using UnityEngine;

namespace Clione.Example
{
    public class Window1Presenter : WindowPresenterBase
    {
        [SerializeField] private Window1View _view;

        public override void Initialize()
        {
            _view.Initialize();
            SetEvent();
        }

        private void SetEvent()
        {
            _view.OnClickedMoveScreenAction += MoveScreen;
        }

        private static void MoveScreen(Scene1ScreenType type)
        {
            SceneLoader.Instance.LoadScreen(TestResourcePrefabPath.GetScreenPath(type.ToString()));
        }
    }
}
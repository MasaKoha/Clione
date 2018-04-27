using UnityEngine;

namespace Clione.Example
{
    public class Scene1Presenter : ScenePresenterBase
    {
        [SerializeField] private Scene1View _view;

        private void Start()
        {
            SceneLoader.Instance.Initialize();
        }

        public override void Initialize(object param)
        {
            _view.Initialize();
            SetEvent();
        }

        public override void InitializeOpenWindowAndScreen()
        {
            OpenWindow(Scene1WindowType.Window1, Scene1ScreenType.Screen1);
        }

        private void SetEvent()
        {
            _view.Window1ButtonClickedEvent.AddListener(() =>
                OpenWindow(Scene1WindowType.Window1, Scene1ScreenType.Screen1));

            _view.Window2ButtonClickedEvent.AddListener(() =>
                OpenWindow(Scene1WindowType.Window2, Scene1ScreenType.Screen1));

            _view.Scene2ButtonClickedEvent.AddListener(() => SceneLoader.Instance.LoadScene("Scene2"));
        }

        private static void OpenWindow(Scene1WindowType windowType, Scene1ScreenType screenType)
        {
            string windowPath = ExampleResourcePrefabPath.GetWindowPath(windowType.ToString());
            string screenPath = ExampleResourcePrefabPath.GetScreenPath(screenType.ToString());
            SceneLoader.Instance.LoadWindow(windowPath, screenPath);
        }
    }
}
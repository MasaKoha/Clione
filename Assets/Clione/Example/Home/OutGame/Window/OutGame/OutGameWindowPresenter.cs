using System.Collections;
using Clione.Home;
using UnityEngine;

namespace Clione.Example
{
    public class OutGameWindowPresenter : WindowBase
    {
        [SerializeField] private OutGameWindowView _view;

        public override IEnumerator Initialize()
        {
            _view.Initialize();
            SetEvent();
            yield break;
        }

        private void SetEvent()
        {
            _view.OnClickedMoveScreenAction += MoveScreen;
            _view.OnClickedInGameButtonAction += () =>
            {
                SceneLoader.LoadWindow(
                    ExampleResourcePrefabPath.GetWindowPath(OutGameWindowType.PreBattleWindow.ToString()),
                    ExampleResourcePrefabPath.GetScreenPath(OutGameScreenType.PreBattleScreen.ToString())
                );
            };
        }

        private static void MoveScreen(OutGameScreenType type)
        {
            SceneLoader.LoadScreen(ExampleResourcePrefabPath.GetScreenPath(type.ToString()));
        }
    }
}
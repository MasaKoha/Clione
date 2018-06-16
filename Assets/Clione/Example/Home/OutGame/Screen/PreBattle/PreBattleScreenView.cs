using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class PreBattleScreenView : OutGameScreenViewBase
    {
        [SerializeField] private Button _gameStartButton;

        public Button.ButtonClickedEvent GameStartButtonClickedEvent => _gameStartButton.onClick;
    }
}
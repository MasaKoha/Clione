using System;
using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class OutGameWindowView : MonoBehaviour
    {
        [SerializeField] private Button _homeButton;
        [SerializeField] private Button _domesticButton;
        [SerializeField] private Button _trainingButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _inGameButton;

        public event Action<OutGameScreenType> OnClickedMoveScreenAction;
        public event Action OnClickedInGameButtonAction;


        public void Initialize()
        {
            SetEvent();
        }

        private void SetEvent()
        {
            _homeButton.onClick.AddListener(() => OnClickedButton(OutGameScreenType.HomeScreen));
            _domesticButton.onClick.AddListener(() => OnClickedButton(OutGameScreenType.DomesticScreen));
            _trainingButton.onClick.AddListener(() => OnClickedButton(OutGameScreenType.TrainingScreen));
            _shopButton.onClick.AddListener(() => OnClickedButton(OutGameScreenType.ShopScreen));
            _inGameButton.onClick.AddListener(() => OnClickedInGameButtonAction?.Invoke());
        }

        private void OnClickedButton(OutGameScreenType type)
        {
            OnClickedMoveScreenAction?.Invoke(type);
        }
    }
}
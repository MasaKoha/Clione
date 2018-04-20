using System;
using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class Window1View : MonoBehaviour
    {
        [SerializeField] private Button _screen1Button;
        [SerializeField] private Button _screen2Button;
        [SerializeField] private Button _screen3Button;
        [SerializeField] private Button _screen4Button;

        public event Action<Scene1ScreenType> OnClickedMoveScreenAction;

        public void Initialize()
        {
            SetEvent();
        }

        private void SetEvent()
        {
            _screen1Button.onClick.AddListener(() => OnClickedBUtton(Scene1ScreenType.Screen1));
            _screen2Button.onClick.AddListener(() => OnClickedBUtton(Scene1ScreenType.Screen2));
            _screen3Button.onClick.AddListener(() => OnClickedBUtton(Scene1ScreenType.Screen3));
            _screen4Button.onClick.AddListener(() => OnClickedBUtton(Scene1ScreenType.Screen4));
        }

        private void OnClickedBUtton(Scene1ScreenType type)
        {
            OnClickedMoveScreenAction?.Invoke(type);
        }
    }
}
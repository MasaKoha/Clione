using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class Scene1View : MonoBehaviour
    {
        [SerializeField] private Button _window1Button;

        [SerializeField] private Button _window2Button;

        public Button.ButtonClickedEvent Window1ButtonClickedEvent
        {
            get { return _window1Button.onClick; }
        }

        public Button.ButtonClickedEvent Window2ButtonClickedEvent
        {
            get { return _window2Button.onClick; }
        }

        public void Initialize()
        {
        }
    }
}
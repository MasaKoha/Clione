using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class Scene1View : MonoBehaviour
    {
        [SerializeField] private Button _window1Button;

        [SerializeField] private Button _window2Button;

        [SerializeField] private Button _scene2Button;

        public Button.ButtonClickedEvent Window1ButtonClickedEvent => _window1Button.onClick;

        public Button.ButtonClickedEvent Window2ButtonClickedEvent => _window2Button.onClick;

        public Button.ButtonClickedEvent Scene2ButtonClickedEvent => _scene2Button.onClick;

        public void Initialize()
        {
        }
    }
}
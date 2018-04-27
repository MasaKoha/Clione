using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class DrillDownViewer1View : MonoBehaviour
    {
        [SerializeField] private Button _nextButton;

        public Button.ButtonClickedEvent NextButtonClickedEvent => _nextButton.onClick;

        [SerializeField] private Button _backButton;

        public Button.ButtonClickedEvent BackButtonClickedEvent => _backButton.onClick;

        public void Initialize()
        {
        }
    }
}
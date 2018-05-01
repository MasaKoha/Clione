using Clione.DrillDown;
using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class DrillDownExampleMain : MonoBehaviour
    {
        [SerializeField] private Transform _drillViewParent;

        [SerializeField] private Button _showDrillViewButton;

        private void Start()
        {
            DrillDownViewerManager.Instance.Initialize(_drillViewParent);
            SetEvent();
        }

        private void SetEvent()
        {
            _showDrillViewButton.onClick.AddListener(() =>
                DrillDownViewerManager.Instance.Show(ExampleResourcePrefabPath.GetDrillDownViewerPath("DrillDownViewer1"),
                    null));
        }
    }
}
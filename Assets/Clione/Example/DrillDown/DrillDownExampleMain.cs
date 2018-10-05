using Clione.Core;
using Clione.DrillDown;
using Clione.ResourceLoader;
using UnityEngine;
using UnityEngine.UI;

namespace Clione.Example
{
    public class DrillDownExampleMain : MonoBehaviour
    {
        [SerializeField] private Transform _drillViewParent;

        [SerializeField] private Button _showDrillViewButton;

        [SerializeField] private Button _clearButton;

        private DrillDownViewerManager _manager;

        private void Start()
        {
            ClioneCore.Initialize(new UnityApiResourceLoader());
            _manager = new DrillDownViewerManager(_drillViewParent);
            SetEvent();
        }

        private void SetEvent()
        {
            _showDrillViewButton.onClick.AddListener(() =>
                _manager.Show(ExampleResourcePrefabPath.GetDrillDownViewerPath("DrillDownViewer1"), null, () =>
                {
                    Debug.Log("Show Complete");
                }));

            _clearButton.onClick.AddListener(() =>
            {
                _manager.Clear(() =>
                {
                    Debug.Log("Clear Complete");
                });
            });
        }
    }
}
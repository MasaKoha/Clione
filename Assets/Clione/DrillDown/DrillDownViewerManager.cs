using System.Collections.Generic;
using Clione.Utility;
using UnityEngine;

namespace Clione.DrillDown
{
    public class DrillDownViewerManager
    {
        private readonly Stack<IDrillDownViewer> _drillDownViewerStack = new Stack<IDrillDownViewer>();

        private readonly Transform _parent;

        public DrillDownViewerManager(Transform parent)
        {
            _parent = parent;
        }

        public void Show(string path, object param)
        {
            var viewerGameObject = Resources.Load<GameObject>(path);
            var iDrillDownViewer = Object.Instantiate(viewerGameObject, _parent).GetComponent<IDrillDownViewer>();

            if (_drillDownViewerStack.Count != 0)
            {
                var currentViewer = _drillDownViewerStack.Peek();
                currentViewer?.Next(true);
            }

            _drillDownViewerStack.Push(iDrillDownViewer);
            iDrillDownViewer.Initialize(param, this);
            iDrillDownViewer.Show();
        }

        public void Back()
        {
            var hideDrillDownViewer = _drillDownViewerStack.Pop();
            hideDrillDownViewer.Next(false);

            if (_drillDownViewerStack.Count == 0)
            {
                return;
            }

            var showDrillDownViewer = _drillDownViewerStack.Peek();
            showDrillDownViewer.Show();
        }

        public void Clear()
        {
            foreach (var viewer in _drillDownViewerStack)
            {
                viewer.Next(false);
            }

            _drillDownViewerStack.Clear();
        }
    }
}
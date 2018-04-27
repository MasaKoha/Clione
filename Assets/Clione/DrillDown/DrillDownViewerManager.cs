using System.Collections.Generic;
using Clione.Utility;
using UnityEngine;

namespace Clione.DrillDown
{
    public class DrillDownViewerManager : Singleton<DrillDownViewerManager>
    {
        private readonly Stack<IDrillDownViewer> _iDrillDownViewerStack = new Stack<IDrillDownViewer>();

        private Transform _parent;

        public void Initialize(Transform parent)
        {
            _parent = parent;
        }

        public void Show(string path, object param)
        {
            var viewerGameObject = Resources.Load<GameObject>(path);
            var iDrillDownViewer = Object.Instantiate(viewerGameObject, _parent).GetComponent<IDrillDownViewer>();

            if (_iDrillDownViewerStack.Count != 0)
            {
                var currentViewer = _iDrillDownViewerStack.Peek();
                currentViewer?.Next(true);
            }

            _iDrillDownViewerStack.Push(iDrillDownViewer);
            iDrillDownViewer.Initialize(param);
            iDrillDownViewer.Show();
        }

        public void Back()
        {
            var hideDrillDownViewer = _iDrillDownViewerStack.Pop();
            hideDrillDownViewer.Next(false);

            if (_iDrillDownViewerStack.Count != 0)
            {
                var showDrillDownViewer = _iDrillDownViewerStack.Peek();
                showDrillDownViewer.Show();
            }
        }
    }
}
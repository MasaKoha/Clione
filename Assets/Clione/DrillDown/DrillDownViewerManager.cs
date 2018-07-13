using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clione.DrillDown
{
    public class DrillDownViewerManager
    {
        private readonly Stack<DrillDownViewerBase> _drillDownViewerList = new Stack<DrillDownViewerBase>();

        private readonly Transform _parent;

        public DrillDownViewerManager(Transform parent)
        {
            _parent = parent;
        }

        /// <summary>
        /// Show Target DrillDownView
        /// </summary>
        public void Show(string drillDownPath, object param, Action onComplete = null)
        {
            var viewerGameObject = Resources.Load<GameObject>(drillDownPath);
            var drillDownViewer = UnityEngine.Object.Instantiate(viewerGameObject, _parent).GetComponent<DrillDownViewerBase>();

            if (_drillDownViewerList.Count != 0)
            {
                var currentViewer = _drillDownViewerList.Peek();
                currentViewer.Next(true);
            }

            _drillDownViewerList.Push(drillDownViewer);
            drillDownViewer.Initialize(param, this);
            drillDownViewer.Show(onComplete);
        }

        /// <summary>
        /// Hide Current DrillDownViewer
        /// </summary>
        public void Hide(Action onComplete = null)
        {
            var hideDrillDownViewer = _drillDownViewerList.Pop();
            hideDrillDownViewer.Next(false);

            if (_drillDownViewerList.Count == 0)
            {
                onComplete?.Invoke();
                return;
            }

            var showDrillDownViewer = _drillDownViewerList.Peek();
            showDrillDownViewer.Show(onComplete);
        }

        /// <summary>
        /// Hide All DrillDownViewer
        /// </summary>
        public void Clear(Action onComplete = null)
        {
            if (_drillDownViewerList.Count == 0)
            {
                Debug.LogError("DrillDownViewerList Stack Count = 0");
                return;
            }

            var hideDrillDownViewer = _drillDownViewerList.Pop();

            foreach (var viewer in _drillDownViewerList)
            {
                UnityEngine.Object.Destroy(viewer.ViewerGameObject);
            }

            hideDrillDownViewer.Next(false, () =>
            {
                _drillDownViewerList.Clear();
                onComplete?.Invoke();
            });
        }

        /// <summary>
        /// Show Target DrillDownView (Coroutine ver.)
        /// </summary>
        public IEnumerator ShowEnumerator(string drillDownPath, object param)
        {
            bool completed = false;
            Show(drillDownPath, param, () => completed = true);
            yield return new WaitUntil(() => completed);
        }

        /// <summary>
        /// Hide Current DrillDownViewer(Coroutine ver.)
        /// </summary>
        public IEnumerator HideEnumerator()
        {
            bool completed = false;
            Hide(() => completed = true);
            yield return new WaitUntil(() => completed);
        }

        /// <summary>
        /// Hide All DrillDownViewer(Coroutine ver.)
        /// </summary>
        public IEnumerator ClearEnumerator()
        {
            bool completed = false;
            Clear(() => completed = true);
            yield return new WaitUntil(() => completed);
        }
    }
}
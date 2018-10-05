using System;
using System.Collections;
using System.Collections.Generic;
using Clione.Core;
using Clione.ResourceLoader;
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

        public void Show(string drillDownPath, object param, Action onComplete)
        {
            ClioneCore.Run(ShowEnumerator(drillDownPath, param, onComplete));
        }

        /// <summary>
        /// Show Target DrillDownView
        /// </summary>
        private IEnumerator ShowEnumerator(string drillDownPath, object param, Action onComplete)
        {
            var loaded = false;
            GameObject prefab = null;

            ClioneResourceLoader.LoadAsync<GameObject>(drillDownPath,
                uiParts =>
                {
                    prefab = uiParts;
                    loaded = true;
                },
                () =>
                {
                    Debug.LogError($"{drillDownPath} is not found.");
                    loaded = true;
                });

            while (!loaded)
            {
                yield return null;
            }

            var drillDownViewer = UnityEngine.Object.Instantiate(prefab, _parent).GetComponent<DrillDownViewerBase>();

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
        public void Hide(Action onComplete)
        {
            var hideDrillDownViewer = _drillDownViewerList.Pop();
            hideDrillDownViewer.Next(false);

            if (_drillDownViewerList.Count == 0)
            {
                onComplete.Invoke();
                return;
            }

            var showDrillDownViewer = _drillDownViewerList.Peek();
            showDrillDownViewer.Show(onComplete);
        }

        /// <summary>
        /// Hide All DrillDownViewer
        /// </summary>
        public void Clear(Action onComplete)
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
                onComplete.Invoke();
            });
        }
    }
}
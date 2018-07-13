using System;
using Clione.DrillDown;
using Clione.Utility;
using UnityEngine;

namespace Clione.Example
{
    public class DrillDownViewer1Presenter : DrillDownViewerBase
    {
        [SerializeField] private DrillDownViewer1View _view;

        private SimpleMoveAnimation _simpleMoveAnimation;

        protected override void OnInitialize(object param)
        {
            _view.Initialize();
            DrillDownViewRectTransform.anchoredPosition =
                new Vector2(ViewWidth, DrillDownViewRectTransform.anchoredPosition.y);
            _simpleMoveAnimation = new SimpleMoveAnimation(this, DrillDownViewRectTransform);
            SetEvent();
        }

        private void SetEvent()
        {
            _view.BackButtonClickedEvent.AddListener(() => { DrillDownManager.Hide(() => { Debug.Log("HideComplete"); }); });

            _view.NextButtonClickedEvent.AddListener(() =>
            {
                DrillDownManager.Show(ExampleResourcePrefabPath.GetDrillDownViewerPath("DrillDownViewer1")
                    , null
                    , () => { Debug.Log("Show Complete"); });
            });
        }

        protected override void OnShow(Action onComplete)
        {
            this.gameObject.SetActive(true);
            _simpleMoveAnimation.MoveX(0, 0.3f, onComplete);
        }

        protected override void OnDig(Action onComplete)
        {
            _simpleMoveAnimation.MoveX(-ViewWidth * 1, 0.3f, onComplete);
        }

        protected override void OnUndig(Action onComplete)
        {
            _simpleMoveAnimation.MoveX(-ViewWidth * -1, 0.3f, () =>
            {
                this.gameObject.SetActive(false);
                onComplete?.Invoke();
            });
        }
    }
}
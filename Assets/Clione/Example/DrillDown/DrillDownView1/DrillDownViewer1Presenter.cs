using Clione.DrillDown;
using Clione.Utility;
using UnityEngine;

namespace Clione.Example
{
    public class DrillDownViewer1Presenter : DrillDownViewer
    {
        [SerializeField] private DrillDownViewer1View _view;

        private SimpleMoveAnimation _simpleMoveAnimation;

        public override void Initialize(object param)
        {
            _view.Initialize();
            base.Initialize(param);
            DrillDownViewRectTransform.anchoredPosition =
                new Vector2(ViewWidth, DrillDownViewRectTransform.anchoredPosition.y);
            _simpleMoveAnimation = new SimpleMoveAnimation(this, DrillDownViewRectTransform);
            SetEvent();
        }

        private void SetEvent()
        {
            _view.BackButtonClickedEvent.AddListener(() => { DrillDownViewerManager.Instance.Back(); });
            _view.NextButtonClickedEvent.AddListener(() =>
            {
                DrillDownViewerManager.Instance.Show(
                    ExampleResourcePrefabPath.GetDrillDownViewerPath("DrillDownViewer1"),
                    null);
            });
        }

        public override void Show()
        {
            this.gameObject.SetActive(true);
            _simpleMoveAnimation.MoveX(0);
        }

        public override void Next(bool isDig)
        {
            _simpleMoveAnimation.MoveX(-ViewWidth * (isDig ? 1 : -1), 0.3f, () =>
            {
                this.gameObject.SetActive(false);
                if (!isDig)
                {
                    Destroy(this.gameObject);
                }
            });
        }
    }
}
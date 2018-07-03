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
            _view.BackButtonClickedEvent.AddListener(() => { DrillDownManager.Back(); });
            _view.NextButtonClickedEvent.AddListener(() =>
            {
                DrillDownManager.Show(
                    ExampleResourcePrefabPath.GetDrillDownViewerPath("DrillDownViewer1"),
                    null);
            });
        }

        protected override void OnShow()
        {
            this.gameObject.SetActive(true);
            _simpleMoveAnimation.MoveX(0);
        }

        protected override void OnDig()
        {
            _simpleMoveAnimation.MoveX(-ViewWidth * 1, 0.3f, () => { this.gameObject.SetActive(false); });
        }

        // TODO:変数名を要確認
        protected override void OnUndig()
        {
            _simpleMoveAnimation.MoveX(-ViewWidth * -1, 0.3f, () =>
            {
                this.gameObject.SetActive(false);
                Destroy(this.gameObject);
            });
        }
    }
}
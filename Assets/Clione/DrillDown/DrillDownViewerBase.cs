using UnityEngine;

namespace Clione.DrillDown
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class DrillDownViewerBase : MonoBehaviour, IDrillDownViewer
    {
        protected RectTransform DrillDownViewRectTransform;

        protected float ViewWidth;

        public DrillDownViewerManager DrillDownManager { get; private set; }

        public virtual void Initialize(object param, DrillDownViewerManager manager)
        {
            DrillDownManager = manager;
            DrillDownViewRectTransform = this.GetComponent<RectTransform>();
            ViewWidth = DrillDownViewRectTransform.rect.width;
            OnInitialize(param);
        }

        protected abstract void OnInitialize(object param);

        public void Show()
        {
            OnShow();
        }

        protected abstract void OnShow();

        public void Next(bool isDig)
        {
            if (isDig)
            {
                OnDig();
            }
            else
            {
                OnUndig();
            }
        }

        protected abstract void OnDig();

        protected abstract void OnUndig();
    }
}
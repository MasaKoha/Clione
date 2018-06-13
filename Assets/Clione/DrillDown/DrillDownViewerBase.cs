using UnityEngine;

namespace Clione.DrillDown
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class DrillDownViewerBase : MonoBehaviour, IDrillDownViewer
    {
        protected RectTransform DrillDownViewRectTransform;

        protected float ViewWidth;

        public DrillDownViewerManager Manager { get; private set; }

        public virtual void Initialize(object param, DrillDownViewerManager manager)
        {
            Manager = manager;
            DrillDownViewRectTransform = this.GetComponent<RectTransform>();
            ViewWidth = DrillDownViewRectTransform.rect.width;
            OnInitialize(param);
        }

        protected abstract void OnInitialize(object param);

        public abstract void Show();

        public abstract void Next(bool isDig);
    }
}
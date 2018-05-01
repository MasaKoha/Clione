using UnityEngine;

namespace Clione.DrillDown
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class DrillDownViewer : MonoBehaviour, IDrillDownViewer
    {
        protected RectTransform DrillDownViewRectTransform;

        protected float ViewWidth;

        public virtual void Initialize(object param)
        {
            DrillDownViewRectTransform = this.GetComponent<RectTransform>();
            ViewWidth = DrillDownViewRectTransform.rect.width;
        }

        public abstract void Show();

        public abstract void Next(bool isDig);
    }
}
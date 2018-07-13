using System;
using UnityEngine;

namespace Clione.DrillDown
{
    [RequireComponent(typeof(RectTransform))]
    public abstract class DrillDownViewerBase : MonoBehaviour
    {
        protected RectTransform DrillDownViewRectTransform;

        protected float ViewWidth;

        protected DrillDownViewerManager DrillDownManager { get; private set; }

        public GameObject ViewerGameObject => this.gameObject;

        public virtual void Initialize(object param, DrillDownViewerManager manager)
        {
            DrillDownManager = manager;
            DrillDownViewRectTransform = this.GetComponent<RectTransform>();
            ViewWidth = DrillDownViewRectTransform.rect.width;
            OnInitialize(param);
        }

        protected abstract void OnInitialize(object param);

        public void Show(Action onComplete)
        {
            OnShow(onComplete);
        }

        protected abstract void OnShow(Action onComplete);

        public void Next(bool isDig, Action onComplete = null)
        {
            if (isDig)
            {
                OnDig(() =>
                {
                    this.ViewerGameObject.SetActive(false);
                    onComplete?.Invoke();
                });
            }
            else
            {
                OnUndig(() =>
                {
                    Destroy(this.ViewerGameObject);
                    onComplete?.Invoke();
                });
            }
        }

        protected abstract void OnDig(Action onComplete);

        protected abstract void OnUndig(Action onComplete);
    }
}
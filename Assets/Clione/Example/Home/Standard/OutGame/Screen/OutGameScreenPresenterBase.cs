using System.Collections;
using Clione.Home;
using Clione.Utility;
using UnityEngine;

namespace Clione.Example
{
    public abstract class OutGameScreenPresenterBase<T> : ScreenBase where T : OutGameScreenViewBase
    {
        [SerializeField] protected T View;

        private SimpleMoveAnimation _animation;

        private RectTransform _rectTransform;

        private float _screenWidth;

        public override IEnumerator InitializeEnumerator()
        {
            _rectTransform = this.GetComponent<RectTransform>();
            _screenWidth = _rectTransform.rect.width;
            View.Initialize();
            _rectTransform.anchoredPosition = new Vector2(_screenWidth, _rectTransform.anchoredPosition.y);
            _animation = new SimpleMoveAnimation(this.GetComponent<MonoBehaviour>(), _rectTransform);
            yield break;
        }

        public override IEnumerator OnOpenScreenEnumerator()
        {
            yield return StartCoroutine(_animation.MoveXEnumerator(0));
        }

        public override IEnumerator OnCloseScreenEnumerator()
        {
            yield return StartCoroutine(_animation.MoveXEnumerator(-_screenWidth));
        }
    }
}
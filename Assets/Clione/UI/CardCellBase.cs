using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEngine.UI.ScrollRect;

namespace Clione.UI
{
    public abstract class CardCellBase : MonoBehaviour, ICard, IBeginDragHandler, IEndDragHandler,
IDragHandler
    {
        private CardSwipeEvent _cardEvent = new CardSwipeEvent();

        public CardSwipeEvent CardEvent => _cardEvent;

        private OnSwipeValueChangedEvent _swipeEvent = new OnSwipeValueChangedEvent();

        public OnSwipeValueChangedEvent SwipeValueChangedEvent => _swipeEvent;

        [SerializeField] private Button _button;

        private ScrollRect _scrollRect;

        private RectTransform _scrollRectContent;

        private float _width;

        private float _height;

        private float _thresholdX;

        private float _thresholdY;

        public void Initialize(float threshold)
        {
            _scrollRect = this.GetComponent<ScrollRect>();
            var scrollRectTransform = this.GetComponent<RectTransform>();

            _width = scrollRectTransform.rect.width;
            _height = scrollRectTransform.rect.height;
            _scrollRectContent = _scrollRect.content;

            _thresholdX = _width * threshold;
            _thresholdY = _height * threshold;

            _button.onClick.AddListener(() =>
            {
                const float scrollThreshold = 0.1f;
                if (Mathf.Abs(_scrollRectContent.anchoredPosition.x) < scrollThreshold && Mathf.Abs(_scrollRectContent.anchoredPosition.y) < scrollThreshold)
                {
                    OnClick();
                }
            });
        }

        public virtual void OnZeroPosition(RectTransform content)
        {
        }

        public virtual void OnDecideAnimation(RectTransform content, CardState e)
        {
        }

        public void OnClick()
        {
            _cardEvent.Invoke(CardState.Click);
        }

        void IBeginDragHandler.OnBeginDrag(PointerEventData eventData)
        {
        }

        void IDragHandler.OnDrag(PointerEventData eventData)
        {
            var x = _scrollRectContent.anchoredPosition.x / _width;
            var y = _scrollRectContent.anchoredPosition.y / _height;
            var vec = new Vector2(x, y);
            SwipeValueChangedEvent.Invoke(vec);
        }

        void IEndDragHandler.OnEndDrag(PointerEventData eventData)
        {
            CardState cardState = 0;

            if (_thresholdX < _scrollRectContent.anchoredPosition.x)
            {
                cardState |= CardState.Right;
            }

            if (_scrollRectContent.anchoredPosition.x < -_thresholdX)
            {
                cardState |= CardState.Left;
            }

            if (_thresholdY < _scrollRectContent.anchoredPosition.y)
            {
                cardState |= CardState.Up;
            }

            if (_scrollRectContent.anchoredPosition.y < -_thresholdY)
            {
                cardState |= CardState.Down;
            }

            if (cardState == 0)
            {
                OnZeroPosition(_scrollRectContent);
            }
            else
            {
                _cardEvent.Invoke(cardState);
                OnDecideAnimation(_scrollRectContent, cardState);
            }
        }
    }
}
﻿using System.Collections;
using Clione.Utility;
using UnityEngine;

namespace Clione.Example
{
    public class OutGameScreenViewBase : MonoBehaviour
    {
        private SimpleMoveAnimation _animation;

        private RectTransform _rectTransform;

        private float _screenWidth;

        public void Initialize()
        {
            _rectTransform = this.GetComponent<RectTransform>();
            _screenWidth = _rectTransform.rect.width;
            _animation = new SimpleMoveAnimation(this.GetComponent<MonoBehaviour>(), _rectTransform);
            SetScreenInitializePosition();
        }

        public IEnumerator ShowEnumerator()
        {
            var move = _animation.MoveXEnumerator(0);
            while (move.MoveNext())
            {
                yield return null;
            }
        }

        public IEnumerator HideEnumerator()
        {
            var move = _animation.MoveXEnumerator(-_screenWidth);
            while (move.MoveNext())
            {
                yield return null;
            }

            SetScreenInitializePosition();
        }

        private void SetScreenInitializePosition()
        {
            _rectTransform.anchoredPosition = new Vector2(_screenWidth, _rectTransform.anchoredPosition.y);
        }
    }
}
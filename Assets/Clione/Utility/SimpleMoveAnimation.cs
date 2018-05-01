using System;
using System.Collections;
using UnityEngine;

namespace Clione.Utility
{
    public class SimpleMoveAnimation
    {
        private readonly MonoBehaviour _monoBehaviour;

        private readonly RectTransform _rectTransform;

        public SimpleMoveAnimation(MonoBehaviour monoBehaviour, RectTransform rectTransform)
        {
            _monoBehaviour = monoBehaviour;
            _rectTransform = rectTransform;
        }

        public void MoveX(float targetPosition, float time = 0.3f, Action onComplete = null)
        {
            _monoBehaviour.StartCoroutine(MoveXEnumerator(targetPosition, time, onComplete));
        }

        public IEnumerator MoveXEnumerator(float targetPosition, float time = 0.3f, Action onComplete = null)
        {
            float tempTime = 0;
            float initPositionX = _rectTransform.anchoredPosition.x;
            float initPositionY = _rectTransform.anchoredPosition.y;

            while (tempTime < time)
            {
                tempTime += Time.deltaTime;
                float progress = tempTime / time;

                if (progress > 1)
                {
                    progress = 1;
                }

                _rectTransform.anchoredPosition =
                    new Vector2(progress * (targetPosition - initPositionX) + initPositionX, initPositionY);
                yield return null;
            }

            _rectTransform.anchoredPosition = new Vector2(targetPosition, initPositionY);
            onComplete?.Invoke();
        }
    }
}
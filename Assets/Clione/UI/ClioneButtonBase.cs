using System.Collections;
using UnityEngine.UI;
using UnityEngine;

namespace Clione.UI
{
    public abstract class ClioneButtonBase : Button
    {
        private ButtonState _clickState = ButtonState.None;

        private float _longClickTimeDuration = 0.5f;

        protected void SetLongTapTimer(float longClickDuration)
        {
            _longClickTimeDuration = longClickDuration;
        }

        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            switch (state)
            {
                case Selectable.SelectionState.Highlighted:
                    ChangeState(ClickEvent.Release);
                    break;
                case SelectionState.Normal:
                    ChangeState(ClickEvent.Release);
                    break;
                case Selectable.SelectionState.Pressed:
                    ChangeState(ClickEvent.Click);
                    break;
            }
        }

        private enum ClickEvent
        {
            None,
            Click,
            Release,
        }

        private void ChangeState(ClickEvent clickEvent)
        {
            switch (_clickState)
            {
                case ButtonState.None:
                    switch (clickEvent)
                    {
                        case ClickEvent.Click:
                            _clickState = ButtonState.ClickDown;
                            OnClickDown();
                            StartCoroutine(LongClickEnumerator());
                            break;
                    }

                    break;
                case ButtonState.ClickDown:

                    switch (clickEvent)
                    {
                        case ClickEvent.Release:
                            _clickState = ButtonState.None;
                            OnClickUp();
                            break;
                    }

                    break;
                case ButtonState.LongClick:
                    switch (clickEvent)
                    {
                        case ClickEvent.Release:
                            _clickState = ButtonState.None;
                            OnClickUp();
                            break;
                    }

                    break;
            }
        }

        private IEnumerator LongClickEnumerator()
        {
            float clickTime = 0;

            while (clickTime < _longClickTimeDuration)
            {
                if (_clickState == ButtonState.None)
                {
                    yield break;
                }

                clickTime += Time.deltaTime;
                yield return null;
            }

            _clickState = ButtonState.LongClick;
            OnLongClick();
        }

        protected abstract void OnClickDown();

        protected abstract void OnClickUp();

        protected abstract void OnLongClick();
    }
}
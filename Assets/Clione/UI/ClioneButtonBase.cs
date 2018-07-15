using UnityEngine;
using UnityEngine.EventSystems;

namespace Clione.UI
{
    public abstract class ClioneButtonBase : UnityEngine.UI.Button
    {
        private ButtonState _clickState = ButtonState.None;

        private float _startLongTapDuration = 0.5f;

        private float _longTapTime;

        private bool _longTapFirstFrame = true;

        private bool _isDetermining = false;

        public void SetStartLongTapDuration(float duration)
        {
            _startLongTapDuration = duration;
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);

            _longTapTime = 0;
            _longTapFirstFrame = true;
            _isDetermining = false;
            if (_clickState == ButtonState.None)
            {
                return;
            }

            if (_clickState == ButtonState.ClickDown)
            {
                OnClickUp();
                _clickState = ButtonState.None;
                return;
            }

            if (_clickState == ButtonState.LongClick)
            {
                OnEndLongTap();
                OnClickUp();
            }

            _clickState = ButtonState.ClickUp;
        }

        public override void OnPointerDown(PointerEventData eventData)
        {
            base.OnPointerDown(eventData);

            if (!interactable)
            {
                return;
            }

            _clickState = ButtonState.ClickDown;
            _isDetermining = true;
            OnClickDown();
        }

        public override void OnPointerUp(PointerEventData eventData)
        {
            base.OnPointerUp(eventData);

            if (_clickState == ButtonState.LongClick)
            {
                OnEndLongTap();
            }

            if (_clickState != ButtonState.None)
            {
                _longTapTime = 0;
                _clickState = ButtonState.None;
                OnClickUp();
            }

            if (_isDetermining)
            {
                _isDetermining = false;
                OnDecide();
            }
        }

        protected void Update()
        {
            if (_clickState == ButtonState.None)
            {
                return;
            }

            if (_clickState == ButtonState.ClickUp)
            {
                _clickState = ButtonState.None;
                return;
            }

            _longTapTime += Time.deltaTime;

            if (_longTapTime > _startLongTapDuration)
            {
                if (_clickState == ButtonState.ClickUp)
                {
                    OnEndLongTap();
                    _longTapTime = 0;
                }

                if (_longTapFirstFrame)
                {
                    OnStartLongTap();
                    _longTapFirstFrame = false;
                    _clickState = ButtonState.LongClick;
                }
                else
                {
                    OnLongTap();
                }
            }
        }

        protected abstract void OnClickDown();

        protected abstract void OnClickUp();

        protected abstract void OnDecide();

        protected abstract void OnStartLongTap();

        protected abstract void OnLongTap();

        protected abstract void OnEndLongTap();
    }
}
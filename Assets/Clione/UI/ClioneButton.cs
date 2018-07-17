using UnityEngine.Events;

namespace Clione.UI
{
    public class ClioneButtonEvent : UnityEvent<ButtonEventType>
    {
    }

    public class ClioneButton : ClioneButtonBase
    {
        private ButtonEventType _eventBitCode = ButtonEventType.None;

        public UnityEvent<ButtonEventType> ButtonEvent = new ClioneButtonEvent();

        public UnityEvent OnClickDownEvent = new UnityEvent();
        public UnityEvent OnStartLongTapEvent = new UnityEvent();
        public UnityEvent OnLongTapEvent = new UnityEvent();
        public UnityEvent OnEndLongTapEvent = new UnityEvent();
        public UnityEvent OnDecideEvent = new UnityEvent();
        public UnityEvent OnClickUpEvent = new UnityEvent();

        public new UnityEvent onClick => OnDecideEvent;

        protected new void Update()
        {
            base.Update();

            if (_eventBitCode == ButtonEventType.None)
            {
                return;
            }

            if ((_eventBitCode & ButtonEventType.ClickDown) == ButtonEventType.ClickDown)
            {
                interactable = false;
                _eventBitCode &= ~ButtonEventType.ClickDown;
                ButtonEvent.Invoke(ButtonEventType.ClickDown);
                OnClickDownEvent.Invoke();
            }
            else if ((_eventBitCode & ButtonEventType.StartLongTap) == ButtonEventType.StartLongTap)
            {
                _eventBitCode &= ~ButtonEventType.StartLongTap;
                ButtonEvent.Invoke(ButtonEventType.StartLongTap);
                OnStartLongTapEvent.Invoke();
            }
            else if ((_eventBitCode & ButtonEventType.LongTap) == ButtonEventType.LongTap)
            {
                _eventBitCode &= ~ButtonEventType.LongTap;
                ButtonEvent.Invoke(ButtonEventType.LongTap);
                OnLongTapEvent.Invoke();
            }
            else if ((_eventBitCode & ButtonEventType.EndLongTap) == ButtonEventType.EndLongTap)
            {
                _eventBitCode &= ~ButtonEventType.EndLongTap;
                ButtonEvent.Invoke(ButtonEventType.EndLongTap);
                OnEndLongTapEvent.Invoke();
            }
            else if ((_eventBitCode & ButtonEventType.Decide) == ButtonEventType.Decide)
            {
                _eventBitCode &= ~ButtonEventType.Decide;
                ButtonEvent.Invoke(ButtonEventType.Decide);
                OnDecideEvent.Invoke();
            }
            else if ((_eventBitCode & ButtonEventType.ClickUp) == ButtonEventType.ClickUp)
            {
                interactable = true;
                _eventBitCode &= ~ButtonEventType.ClickUp;
                ButtonEvent.Invoke(ButtonEventType.ClickUp);
                OnClickUpEvent.Invoke();
            }
        }

        protected override void OnClickDown()
        {
            _eventBitCode |= ButtonEventType.ClickDown;
        }

        protected override void OnClickUp()
        {
            _eventBitCode |= ButtonEventType.ClickUp;
        }

        protected override void OnDecide()
        {
            _eventBitCode |= ButtonEventType.Decide;
        }

        protected override void OnStartLongTap()
        {
            _eventBitCode |= ButtonEventType.StartLongTap;
        }

        protected override void OnLongTap()
        {
            _eventBitCode |= ButtonEventType.LongTap;
        }

        protected override void OnEndLongTap()
        {
            _eventBitCode |= ButtonEventType.EndLongTap;
        }
    }
}
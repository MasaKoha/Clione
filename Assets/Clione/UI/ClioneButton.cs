using UnityEngine.Events;

namespace Clione.UI
{
    public class ClioneButtonEvent : UnityEvent<ButtonEventType>
    {
    }

    public class ClioneButton : ClioneButtonBase
    {
        public UnityEvent<ButtonEventType> ButtonEvent = new ClioneButtonEvent();

        private ButtonEventType _eventBitCode = ButtonEventType.None;

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
            }
            else if ((_eventBitCode & ButtonEventType.StartLongTap) == ButtonEventType.StartLongTap)
            {
                _eventBitCode &= ~ButtonEventType.StartLongTap;
                ButtonEvent.Invoke(ButtonEventType.StartLongTap);
            }
            else if ((_eventBitCode & ButtonEventType.LongTap) == ButtonEventType.LongTap)
            {
                _eventBitCode &= ~ButtonEventType.LongTap;
                ButtonEvent.Invoke(ButtonEventType.LongTap);
            }
            else if ((_eventBitCode & ButtonEventType.EndLongTap) == ButtonEventType.EndLongTap)
            {
                _eventBitCode &= ~ButtonEventType.EndLongTap;
                ButtonEvent.Invoke(ButtonEventType.EndLongTap);
            }
            else if ((_eventBitCode & ButtonEventType.Decide) == ButtonEventType.Decide)
            {
                _eventBitCode &= ~ButtonEventType.Decide;
                ButtonEvent.Invoke(ButtonEventType.Decide);
            }
            else if ((_eventBitCode & ButtonEventType.ClickUp) == ButtonEventType.ClickUp)
            {
                interactable = true;
                _eventBitCode &= ~ButtonEventType.ClickUp;
                ButtonEvent.Invoke(ButtonEventType.ClickUp);
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
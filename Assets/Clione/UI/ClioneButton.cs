using UnityEngine.Events;

namespace Clione.UI
{
    public class ClioneButton : ClioneButtonBase
    {
        public UnityEvent onClick => base.onClick;

        public UnityEvent onClickDown;

        public UnityEvent onClickUp;

        public UnityEvent onLongClick;

        protected override void OnClickDown()
        {
            onClickDown.Invoke();
        }

        protected override void OnClickUp()
        {
            onClickUp.Invoke();
        }

        protected override void OnLongClick()
        {
            onLongClick.Invoke();
        }
    }
}
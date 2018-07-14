using System;

namespace Clione.UI
{
    [Flags]
    public enum ButtonEventType
    {
        None = 0,
        ClickDown = 1,
        StartLongTap = 1 << 1,
        LongTap = 1 << 2,
        EndLongTap = 1 << 3,
        Decide = 1 << 4,
        ClickUp = 1 << 5,
    }
}
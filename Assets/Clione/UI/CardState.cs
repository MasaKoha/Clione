using System;

namespace Clione.UI
{
    [Flags]
    public enum CardState
    {
        None = 1,
        Left = 1 << 1,
        Right = 1 << 2,
        Up = 1 << 3,
        Down = 1 << 4,
        Click = 1 << 5,
    }
}
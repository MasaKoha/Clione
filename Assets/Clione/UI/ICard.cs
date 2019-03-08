using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Clione.UI
{
    public interface ICard
    {
        CardSwipeEvent CardEvent { get; }
        OnSwipeValueChangedEvent SwipeValueChangedEvent { get; }
        void Initialize(float threshold);
    }
}
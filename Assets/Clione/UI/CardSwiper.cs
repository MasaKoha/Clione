using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Clione.UI
{
    public class CardSwipeEvent : UnityEvent<CardState> { }

    public class OnSwipeValueChangedEvent : UnityEvent<Vector2> { }

    public class CardSwiper : MonoBehaviour
    {
        private Queue<ICard> _cards;

        public ICard CurrentCard => _cards.Peek();

        public CardSwipeEvent CurrentCardSwipeEvent => CurrentCard.CardEvent;

        public int RemainCardCount => _cards.Count;

        public void Initialize(float threshold, Queue<ICard> cards)
        {
            _cards = cards;
            foreach (var card in _cards)
            {
                card.Initialize(threshold);
            }
        }
    }
}
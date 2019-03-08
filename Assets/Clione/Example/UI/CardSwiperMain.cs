using System.Collections;
using System.Collections.Generic;
using Clione.UI;
using UnityEngine;

namespace Clione.Example.Card
{
    public class CardSwiperMain : MonoBehaviour
    {
        [SerializeField] private CardSwiper _cardSwiper;

        [SerializeField] private TestCard _card;

        private void Start()
        {
            Queue<ICard> a = new Queue<ICard>();
            for (int i = 0; i < 10; i++)
            {
                var card = Instantiate(_card, _cardSwiper.transform).GetComponent<ICard>();
                a.Enqueue(card);
            }
            _cardSwiper.Initialize(0.2f, a);
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using Clione.UI;
using Clione.Utility;
using UnityEngine;

namespace Clione.Example.Card
{
    public class TestCard : CardCellBase
    {
        public override void OnDecideAnimation(RectTransform content, CardState e)
        {
            var animation = new SimpleMoveAnimation(this, content);

            switch (e)
            {
                case CardState.Left:
                    animation.MoveX(-500);
                    break;
                case CardState.Right:
                    animation.MoveX(500);
                    break;
            }
        }

        public override void OnZeroPosition(RectTransform content)
        {
            var animation = new SimpleMoveAnimation(this, content);
            animation.MoveX(0);
        }
    }
}
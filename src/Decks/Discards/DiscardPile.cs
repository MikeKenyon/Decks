using System;
using System.Collections.Generic;
using System.Text;
using Decks.Internal;

namespace Decks
{
    internal class DiscardPile<TElement> : DeckStack<TElement>, IDiscardPile<TElement>, Internal.IDiscardPileInternal<TElement>
        where TElement : class
    {
        public DiscardPile(Deck<TElement> deck) : base(deck)
        {
        }

        void IDiscardPileInternal<TElement>.Add(TElement element, DeckSide side)
        {
            switch (side)
            {
                case DeckSide.Bottom:
                    Contents.Add(element);
                    break;
                case DeckSide.Top:
                    Contents.Insert(0, element);
                    break;
            }
        }

        void IDeckStackInternal<TElement>.Add(TElement element)
        {
            ((IDiscardPileInternal<TElement>)this).Add(element, DeckSide.Top);
        }

        void IDeckStackInternal<TElement>.CheckEnabled()
        {
            // No-Op
        }

        public void Readd(DeckSide side)
        {
            Contents.Apply(c => Deck.DrawPileStack.Add(c,side));
            Contents.Clear();
        }
    }
}

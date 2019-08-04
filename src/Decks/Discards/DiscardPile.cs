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
        void IDeckVisitable<TElement>.Accept(IDeckVisitor<TElement> visitor)
        {
            visitor.Visit(this);
        }

        void IDiscardPileInternal<TElement>.Add(TElement element, DeckSide side)
        {
            switch (side)
            {
                case DeckSide.Bottom:
                    Contents.Add(element);
                    break;
                case DeckSide.Default:
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

        /// <summary>
        /// Adds the discards back to the draw pile (usually on the bottom).
        /// </summary>
        /// <returns>Discards (for fluent purposes)</returns>
        public IDiscardPile<TElement> Readd(DeckSide side)
        {
            Deck.Events.PuttingDiscardsBackIntoDrawPile();

            Contents.Apply(c => Deck.DrawPileStack.Add(c,side));
            Contents.Clear();

            Deck.Events.PutDiscardsBackIntoDrawPile();

            return this;
        }
    }
}

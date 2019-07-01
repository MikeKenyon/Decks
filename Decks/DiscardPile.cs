using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    internal class DiscardPile<TElement> : DeckStack<TElement>, IDiscardPile<TElement>
        where TElement : class
    {
        public DiscardPile(Deck<TElement> deck) : base(deck)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public interface IDiscardPile<TElement> : IDeckStack<TElement>
        where TElement : class
    {
        /// <summary>
        /// Adds the discards back to the draw pile (usually on the bottom).
        /// </summary>
        /// <returns>Discards (for fluent purposes)</returns>
        IDiscardPile<TElement> Readd(DeckSide side = DeckSide.Bottom);
    }
}

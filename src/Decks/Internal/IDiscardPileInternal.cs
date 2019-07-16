using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal
{
    /// <summary>
    /// The internal interface to the discard pile.
    /// </summary>
    /// <typeparam name="TElement">The elements in the discard pile.</typeparam>
    internal interface IDiscardPileInternal<TElement> : IDeckStackInternal<TElement>, IDiscardPile<TElement> where TElement : class
    {
        /// <summary>
        /// Adds an eleemnt from one area to another.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="side">The side to add to.</param>
        void Add(TElement element, DeckSide side);

    }
}

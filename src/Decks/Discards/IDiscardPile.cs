using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    /// <summary>
    /// This is the stack that contains elements that have been discarded from hands, the table, etc.  The discarded elements
    /// can be readded to the <see cref="IDrawPile{TElement}"/>.
    /// </summary>
    /// <typeparam name="TElement">The elements in the discard pile.</typeparam>
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

using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal
{
    /// <summary>
    /// The internal interface for dealing with the draw pile.  
    /// </summary>
    /// <typeparam name="TElement">The elements in the draw pile.</typeparam>
    internal interface IDrawPileInternal<TElement> : IDeckStackInternal<TElement>, IDrawPile<TElement> where TElement : class
    {
        /// <summary>
        /// Adds an eleemnt from one area to another.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="side">The side to add to.</param>
        void Add(TElement element, DeckSide side);

        /// <summary>
        /// Draws a card, removing it from the drawpile.
        /// </summary>
        /// <param name="side">The side of the deck to draw from.</param>
        /// <returns>The drawn card.</returns>
        TElement Draw(DeckSide side = DeckSide.Top);
        /// <summary>
        /// Gets the number of times that a given topdeck has been shuffled.  Some games fix this number.
        /// </summary>
        uint ShuffleCount { get; set; }

    }
}

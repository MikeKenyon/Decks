using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public interface IDrawPile<TElement> : IDeckStack<TElement> where TElement : class 
    {
        /// <summary>
        /// Optionally gets the discards back and then randomly orders the cards.
        /// </summary>
        /// <param name="retreiveDiscards">Whether or not to clear out the discards.</param>
        void Shuffle(bool retreiveDiscards = true);

        /// <summary>
        /// Gets the number of times that a given topdeck has been shuffled.  Some games fix this number.
        /// </summary>
        uint ShuffleCount { get; }
    }
}

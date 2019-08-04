using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public interface IDrawPile<TElement> : IDeckStack<TElement> where TElement : class 
    {
        /// <summary>
        /// The options related to the draw pile.
        /// </summary>
        Configuration.IDrawPileOptions Options { get; }
        /// <summary>
        /// Optionally gets the discards back and then randomly orders the cards.
        /// </summary>
        /// <param name="retreiveDiscards">Whether or not to clear out the discards.</param>
        void Shuffle(bool retreiveDiscards = true);
    }
}

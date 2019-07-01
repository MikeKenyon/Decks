using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public enum Location
    {
        /// <summary>
        /// The set that draws happen from.
        /// </summary>
        TopDeck = 0,
        /// <summary>
        /// The set that have already been processed.
        /// </summary>
        DiscardPile = 1,
        /// <summary>
        /// In a play-hand.
        /// </summary>
        Hand = 2,
        /// <summary>
        /// On the "table".
        /// </summary>
        Table = 3,
        /// <summary>
        /// The visible cards sitting there to be drawn from.
        /// </summary>
        Tableau = 4
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Common
{
    /// <summary>
    /// How the playing cards in a deck should be sorted.
    /// </summary>
    public enum PlayingCardSortOrder
    {
        /// <summary>
        /// In the standard order for poker (default).
        /// </summary>
        PokerOrder,     // Jokers, Spades, Hearts, Clubs, Diamonds
        /// <summary>
        /// In the order that bridge is played in.
        /// </summary>
        BridgeOrder,    // Jokers, Spades, Hearts, Diamonds, Clubs
        /// <summary>
        /// In the order that cards are shipped in a box.
        /// </summary>
        BoxOrder,       // Spades, Diamonds, Clubs, Hearts, Jokers
    }
}

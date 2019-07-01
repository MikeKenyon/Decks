using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Common
{
    public enum PlayingCardSortOrder
    {
        PokerOrder,     // Jokers, Spades, Hearts, Clubs, Diamonds
        BridgeOrder,    // Jokers, Spades, Hearts, Diamonds, Clubs
        BoxOrder,       // Spades, Diamonds, Clubs, Hearts, Jokers
    }
}

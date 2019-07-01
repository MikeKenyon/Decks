using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    /// <summary>
    /// Operations that may or may not be valid for a particular deck.
    /// </summary>
    [Flags]
    public enum ValidOperations
    {
        /// <summary>
        /// Nothing is possible.  While a viable option, it leads to a largely useless
        /// deck.
        /// </summary>
        None        = 0,
        /// <summary>
        /// The deck can be shuffled once, but then it's order is fixed.  Appropriate
        /// for one-time-through sorts of decks.
        /// </summary>
        ShuffleOnce = 1 << 0,
        /// <summary>
        /// The deck can be shuffled repeatedly.  Usually, this brings discards back in 
        /// and reorders the deck.  Pulling in the discards is optional.
        /// </summary>
        Reshuffle   = 1 << 1 | ShuffleOnce,
        /// <summary>
        /// Allows the adding of things to the topdeck.  Without this, all modifications
        /// to the deck need to be done in <see cref="Deck{TElement}.Initialize"/>.
        /// </summary>
        Add         = 1 << 2,
        /// <summary>
        /// Allows people to deal hands and muck those hands.
        /// </summary>
        DealMuck    = 1 << 3,
        /// <summary>
        /// Allows play to the table.  Without this feature, the 
        /// <see cref="Deck{TElement}.Table"/> is disabled and you must play in other enabled
        /// areas.
        /// </summary>
        PlayToTable = 1 << 4,
        /// <summary>
        /// Allows you to draw from the tableau into your hand.
        /// </summary>
        PlayTableauToHand = 1 << 5,
        /// <summary>
        /// Allows you to play from the tableau straight to the table.
        /// </summary>
        PlayTableauToTable = 1 << 6,
        /// <summary>
        /// The deck enables you to do everything.
        /// </summary>
        All = Reshuffle | Add | DealMuck | PlayToTable 
            | PlayTableauToHand | PlayTableauToTable
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    /// <summary>
    /// The deck operations.
    /// </summary>
    public interface IDeckOptions
    {
        /// <summary>
        /// What operations are allowed by this deck.
        /// </summary>
        ValidOperations Allow
        {
            get;
        }
        /// <summary>
        /// The (default) hand size for newly drawn hands.
        /// </summary>
        uint HandSize
        {
            get;
        }
        /// <summary>
        /// Automatically shuffles the deck when you need another card 
        /// and one isn't available.
        /// </summary>
        bool AutoShuffle { get; }
        /// <summary>
        /// The number of elements in the tableau.
        /// </summary>
        uint TableauSize { get; }
        /// <summary>
        /// Whether the system automatically adjusts the tableau after something modifies 
        /// it's contents.
        /// </summary>
        bool TableauMaintainSize { get; }
        /// <summary>
        /// What to do what a tableau gets to be oversized.
        /// </summary>
        TableauOverflowRule TableauOverflow { get; }
    }
}

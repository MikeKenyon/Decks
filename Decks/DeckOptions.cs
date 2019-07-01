using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    public class DeckOptions : IDeckOptions
    {
        /// <summary>
        /// What operations are allowed by this deck.
        /// </summary>
        public ValidOperations Allow { get; set; } = ValidOperations.All;

        /// <summary>
        /// The (default) hand size for newly drawn hands.
        /// </summary>
        public uint HandSize { get; set; } = 5;
        /// <summary>
        /// The number of elements in the tableau, set to 0 to disable the tableau.
        /// </summary>
        public uint TableauSize { get; set; } = 0;

        /// <summary>
        /// Whether the system automatically adjusts the tableau after something modifies 
        /// it's contents.
        /// </summary>
        public bool TableauMaintainSize { get; set; }
        /// <summary>
        /// What to do what a tableau gets to be oversized.
        /// </summary>
        public TableauOverflowRule TableauOverflow { get; set; }

        /// <summary>
        /// In many games, if the tableau cannot safely draw up, it just shrinks in size.
        /// If this option is set to <see langword="true"/> then the tableau cannot bottom
        /// deck you.
        /// </summary>
        public bool TableauDrawsUpSafely { get; set; }

        /// <summary>
        /// Automatically shuffles the deck when you need another card 
        /// and one isn't available.
        /// </summary>
        public bool AutoShuffle { get; set; }
    }
}

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
        /// Automatically shuffles the deck when you need another card 
        /// and one isn't available.
        /// </summary>
        public bool AutoShuffle { get; set; }
    }
}

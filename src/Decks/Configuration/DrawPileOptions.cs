using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    /// <summary>
    /// Options for the draw pile, which is always enabled.
    /// </summary>
    public class DrawPileOptions : IDrawPileOptions
    {
        /// <summary>
        /// The number of times the deck can be reshuffled.  0, 1, 3 and a negative number are the most commonly 
        /// set numbers.  Any negative number is considered to be unlimited.  Default is unlimited.
        /// </summary>
        public int MaximumShuffleCount { get; set; } = -1;
    }
}

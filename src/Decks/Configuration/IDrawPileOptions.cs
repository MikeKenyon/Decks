using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    /// <summary>
    /// Options for the draw pile.  The draw pile is always enabled.
    /// </summary>
    public interface IDrawPileOptions
    {
        /// <summary>
        /// The number of times the deck can be reshuffled.  0, 1, 3 and a negative number are the most commonly 
        /// set numbers.  <see langword="null" /> is unlimited, which is the default.
        /// </summary>
        uint? MaximumShuffleCount { get; } 
    }
}

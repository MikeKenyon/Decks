using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    public class DiscardOptions : IDiscardOptions
    {
        /// <summary>
        /// Automatically shuffles the deck when you need another card 
        /// and one isn't available.
        /// </summary>
        public bool AutoShuffle { get; set; }
    }
}

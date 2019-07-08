using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    public interface IDiscardOptions
    {
        /// <summary>
        /// Automatically shuffles the deck when you need another card 
        /// and one isn't available.
        /// </summary>
        bool AutoShuffle { get; }

    }
}

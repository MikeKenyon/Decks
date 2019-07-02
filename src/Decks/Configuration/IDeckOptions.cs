using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
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
        /// Options for the play table.  This is the common shared space for all players.
        /// </summary>
        ITableOptions Table { get; }

        /// <summary>
        /// Options for the tableau.
        /// </summary>
        ITableauOptions Tableau { get; }
    }
}

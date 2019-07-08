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
        /// Options for how to deal with discards.
        /// </summary>
        IDiscardOptions Discards { get; }

        /// <summary>
        /// Options for player hands.
        /// </summary>
        IHandOptions Hands { get; }

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

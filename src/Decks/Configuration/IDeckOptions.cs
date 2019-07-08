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
        /// If set to <see langword="true"/> the deck is modifiable after the <see cref="Deck{TElement}.Initialize"/> method has finished.Default is 
        /// to allow this.
        /// </summary>
        bool Modifiable { get;  }

        /// <summary>
        /// Options for the <see cref="IDeck{TElement}.DrawPile"/>.
        /// </summary>
        IDrawPileOptions DrawPile { get; }
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

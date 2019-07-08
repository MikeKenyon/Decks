using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    public class DeckOptions : IDeckOptions
    {
        /// <summary>
        /// What operations are allowed by this deck.
        /// </summary>
        public ValidOperations Allow { get; set; } = ValidOperations.All;


        /// <summary>
        /// Options for player hands.
        /// </summary>
        public HandOptions Hands { get; set; } = new HandOptions();
        /// <summary>
        /// Options for player hands.
        /// </summary>
        IHandOptions IDeckOptions.Hands { get { return this.Hands; } }

        /// <summary>
        /// Options for how to deal with discards.
        /// </summary>
        public DiscardOptions Discards { get; set; } = new DiscardOptions();
        /// <summary>
        /// Options for how to deal with discards.
        /// </summary>
        IDiscardOptions IDeckOptions.Discards { get { return this.Discards; } }


        /// <summary>
        /// Options for the play table.  This is the common shared space for all players.
        /// </summary>
        public TableOptions Table { get; set; } = new TableOptions();

        /// <summary>
        /// Options for the play table.  This is the common shared space for all players.
        /// </summary>
        ITableOptions IDeckOptions.Table { get { return this.Table; } }

        /// <summary>
        /// Options for the tableau.
        /// </summary>
        public TableauOptions Tableau { get; set; } = new TableauOptions();

        /// <summary>
        /// Options for the tableau.
        /// </summary>
        ITableauOptions IDeckOptions.Tableau { get { return this.Tableau; } }
    }
}

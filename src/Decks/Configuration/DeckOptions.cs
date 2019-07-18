using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Configuration
{
    public class DeckOptions : IDeckOptions
    {
        /// <summary>
        /// Get the interaction/eventing interface.
        /// </summary>
        public Events.IDeckEvents Events { get; set; }
        /// <summary>
        /// If set to <see langword="true"/> the deck is modifiable after the <see cref="Deck{TElement}.Initialize"/> method has finished.  Default is 
        /// to allow this.
        /// </summary>
        public virtual bool Modifiable { get; set; } = true;

        /// <summary>
        /// Options for the <see cref="IDeck{TElement}.DrawPile"/>.
        /// </summary>
        public DrawPileOptions DrawPile { get; set; } = new DrawPileOptions();
        /// <summary>
        /// Options for the <see cref="IDeck{TElement}.DrawPile"/>.
        /// </summary>
        IDrawPileOptions IDeckOptions.DrawPile { get { return this.DrawPile; } }

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

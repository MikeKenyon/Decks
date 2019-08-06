using Caliburn.Micro;

namespace Decks.Configuration
{
    /// <summary>
    /// The general configuration for this deck.
    /// </summary>
    public class DeckOptions : PropertyChangedBase, IDeckOptions
    {
        #region Data
        private Events.IDeckEvents _events;
        private bool _modifiable = true;
        private DrawPileOptions _drawPile = new DrawPileOptions();
        private DiscardOptions _discards = new DiscardOptions();
        private TableOptions _table = new TableOptions();
        private TableauOptions _tableau = new TableauOptions();
        private HandOptions _hands = new HandOptions();
        #endregion

        /// <summary>
        /// Get the interaction/eventing interface.
        /// </summary>
        public Events.IDeckEvents Events { get { return _events; } set { _events = value; NotifyOfPropertyChange(); } }
        /// <summary>
        /// If set to <see langword="true"/> the deck is modifiable after the <see cref="Deck{TElement}.Initialize"/> method has finished.  Default is 
        /// to allow this.
        /// </summary>
        public virtual bool Modifiable { get { return _modifiable; } set { _modifiable = value; NotifyOfPropertyChange(); } }

        /// <summary>
        /// Options for the <see cref="IDeck{TElement}.DrawPile"/>.
        /// </summary>
        public DrawPileOptions DrawPile { get { return _drawPile; } set { _drawPile = value; NotifyOfPropertyChange(); } }
        /// <summary>
        /// Options for the <see cref="IDeck{TElement}.DrawPile"/>.
        /// </summary>
        IDrawPileOptions IDeckOptions.DrawPile { get { return DrawPile; } }

        /// <summary>
        /// Options for player hands.
        /// </summary>
        public HandOptions Hands { get { return _hands; } set { _hands = value; NotifyOfPropertyChange(); } }
        /// <summary>
        /// Options for player hands.
        /// </summary>
        IHandOptions IDeckOptions.Hands { get { return Hands; } }

        /// <summary>
        /// Options for how to deal with discards.
        /// </summary>
        public DiscardOptions Discards { get { return _discards; } set { _discards = value; NotifyOfPropertyChange(); } }
        /// <summary>
        /// Options for how to deal with discards.
        /// </summary>
        IDiscardOptions IDeckOptions.Discards { get { return Discards; } }


        /// <summary>
        /// Options for the play table.  This is the common shared space for all players.
        /// </summary>
        public TableOptions Table { get { return _table; } set { _table = value; NotifyOfPropertyChange(); } }

        /// <summary>
        /// Options for the play table.  This is the common shared space for all players.
        /// </summary>
        ITableOptions IDeckOptions.Table { get { return Table; } }

        /// <summary>
        /// Options for the tableau.
        /// </summary>
        public TableauOptions Tableau { get { return _tableau; } set { _tableau = value; NotifyOfPropertyChange(); } }

        /// <summary>
        /// Options for the tableau.
        /// </summary>
        ITableauOptions IDeckOptions.Tableau { get { return Tableau; } }
    }
}

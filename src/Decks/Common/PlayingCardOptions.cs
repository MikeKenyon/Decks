using Decks.Configuration;

namespace Decks.Common
{
    /// <summary>
    /// The options for dealing with a deck.
    /// </summary>
    public class PlayingCardOptions : DeckOptions
    {
        private bool _jokers;
        private PlayingCardSortOrder _order = PlayingCardSortOrder.PokerOrder;
        private AceMode _ace = AceMode.High;
        /// <summary>
        /// Whether this deck contains the 2 jokers or not.
        /// </summary>
        public bool HasJokers { get { return _jokers; } set { _jokers = value; NotifyOfPropertyChange(); } }
        /// <summary>
        /// The sort order for this deck.
        /// </summary>
        public PlayingCardSortOrder Order { get { return _order; } set { _order = value; NotifyOfPropertyChange(); } }
        /// <summary>
        /// How this deck treates Ace.
        /// </summary>
        public AceMode AceMode { get { return _ace; } set { _ace = value; NotifyOfPropertyChange(); } }
    }
}
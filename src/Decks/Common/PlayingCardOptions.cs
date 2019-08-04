using Decks.Configuration;

namespace Decks.Common
{
    public class PlayingCardOptions : DeckOptions
    {
        private bool _jokers;
        private PlayingCardSortOrder _order = PlayingCardSortOrder.PokerOrder;
        private AceMode _ace = AceMode.High;

        public bool HasJokers { get { return _jokers; } set { _jokers = value; NotifyOfPropertyChange(); } }
        public PlayingCardSortOrder Order { get { return _order; } set { _order = value; NotifyOfPropertyChange(); } }
        public AceMode AceMode { get { return _ace; } set { _ace = value; NotifyOfPropertyChange(); } }
    }
}
using Decks.Configuration;

namespace Decks.Common
{
    public class PlayingCardOptions : DeckOptions
    {
        public bool HasJokers { get; set; }
        public PlayingCardSortOrder Order { get; set; }
        public AceMode AceMode { get; set; }
    }
}
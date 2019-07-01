using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Common
{
    public class PinochleDeck : StandardCardDeck
    {
        public PinochleDeck(PinochleOptions options) : base(options)
        {
            ValidateOptions();
        }

        private void ValidateOptions()
        {
            if(CardOptions.AceMode != AceMode.High)
            {
                throw new ArgumentException("AceMode should always be 'High' for a pinochle deck.", "AceMode");
            }
            if (CardOptions.HasJokers)
            {
                throw new ArgumentException("Pinochle decks don't have jokers.", "HasJokers");
            }
        }

        protected override void Initialize()
        {
            var options = CardOptions;
            for (int rank = 9; rank <= (int)PlayingCardRank.King; ++rank)
            {
                for (int suit = (int)PlayingCardSuit.Clubs;
                    suit <= (int)PlayingCardSuit.Spades; ++suit)
                {
                    // Add two of each card.
                    Add(new PlayingCard(options,
                        (PlayingCardSuit)suit, (PlayingCardRank)rank));
                    Add(new PlayingCard(options,
                        (PlayingCardSuit)suit, (PlayingCardRank)rank));
                }
            }
            // Aces
            for (int suit = (int)PlayingCardSuit.Clubs;
                suit <= (int)PlayingCardSuit.Spades; ++suit)
            {
                // Add two of each card.
                Add(new PlayingCard(options,
                    (PlayingCardSuit)suit, PlayingCardRank.Ace));
                Add(new PlayingCard(options,
                    (PlayingCardSuit)suit, PlayingCardRank.Ace));
            }
        }
    }
}

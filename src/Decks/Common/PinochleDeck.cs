using System;

namespace Decks.Common
{
    /// <summary>
    /// A representation of a deck to play pinochle with.
    /// </summary>
    public class PinochleDeck : StandardCardDeck
    {
        /// <summary>
        /// Creates a pinochle deck.
        /// </summary>
        /// <param name="options">The options on how to play with a pinochle deck.</param>
        /// <param name="doInitialize">Should the deck be initialized.</param>
        /// <exception cref="ArgumentException">The options are inappropriate for a pinochle deck.</exception>
        public PinochleDeck(PinochleOptions options, bool doInitialize = true) : base(options, doInitialize)
        {
            CheckOptions();
        }

        /// <summary>
        /// Signaled when the options are updated.
        /// </summary>
        protected override void OnOptionsUpdated()
        {
            base.OnOptionsUpdated();
            CheckOptions();
        }

        /// <summary>
        /// Validates the given options.
        /// </summary>
        private void CheckOptions()
        {
            if (CardOptions.AceMode != AceMode.High)
            {
                throw new ArgumentException("AceMode should always be 'High' for a pinochle deck.", "AceMode");
            }
            if (CardOptions.HasJokers)
            {
                throw new ArgumentException("Pinochle decks don't have jokers.", "HasJokers");
            }
        }

        /// <summary>
        /// Initializes the deck.
        /// </summary>
        protected override void Initialize()
        {
            var options = CardOptions;
            for (var rank = 9; rank <= (int)PlayingCardRank.King; ++rank)
            {
                for (var suit = (int)PlayingCardSuit.Clubs;
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
            for (var suit = (int)PlayingCardSuit.Clubs;
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

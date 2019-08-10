using Newtonsoft.Json;

namespace Decks.Common
{
    /// <summary>
    /// A specialized deck of <see cref="PlayingCard"/>.
    /// </summary>
    public class StandardCardDeck : Deck<PlayingCard>
    {
        /// <summary>
        /// Creates a deck of playing cards.
        /// </summary>
        /// <param name="options">The options used to create the deck.</param>
        /// <param name="doInitialize">Whether or initialize this deck's contents or not.</param>
        public StandardCardDeck(PlayingCardOptions options, bool doInitialize = true) : base(options, doInitialize)
        {
        }

        /// <summary>
        /// How to initialize this deck.
        /// </summary>
        protected override void Initialize()
        {
            var options = CardOptions;
            for (var rank = 1; rank < (int)PlayingCardRank.Joker; ++rank)
            {
                for (var suit = (int)PlayingCardSuit.Clubs;
                    suit <= (int)PlayingCardSuit.Spades; ++suit)
                {
                    Add(new PlayingCard(options,
                        (PlayingCardSuit)suit, (PlayingCardRank)rank));
                }
            }
            if (options.HasJokers)
            {
                Add(new PlayingCard(options, PlayingCardSuit.Black, PlayingCardRank.Joker));
                Add(new PlayingCard(options, PlayingCardSuit.Red, PlayingCardRank.Joker));
            }
        }

        /// <summary>
        /// How to handle rehydration from being serialized.
        /// </summary>
        public override void Rehydrated()
        {
            base.Rehydrated();
            DrawPile.Apply(c => c.Options = CardOptions);
            DiscardPile.Apply(c => c.Options = CardOptions);
            Table.Apply(c => c.Options = CardOptions);
            Tableau.Apply(c => c.Options = CardOptions);
            Hands.Apply(h => h.Apply(c => c.Options = CardOptions));
        }

        /// <summary>
        /// A quick accessor for the specialized options.
        /// </summary>
        protected PlayingCardOptions CardOptions
        {
            get
            {
                return (PlayingCardOptions)Options;
            }
        }
    }
}

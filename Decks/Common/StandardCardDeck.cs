using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Common
{
    public class StandardCardDeck : Deck<PlayingCard>
    {
        public StandardCardDeck(PlayingCardOptions options) : base(options)
        {
        }

        protected override void Initialize()
        {
            var options = CardOptions;
            for (int rank = 1; rank < (int)PlayingCardRank.Joker; ++rank)
            {
                for(int suit = (int)PlayingCardSuit.Clubs; 
                    suit <= (int)PlayingCardSuit.Spades; ++suit)
                {
                    Add(new PlayingCard(options,
                        (PlayingCardSuit)suit, (PlayingCardRank)rank));
                }
            }
            if(options.HasJokers)
            {
                Add(new PlayingCard(options, PlayingCardSuit.Black, PlayingCardRank.Joker));
                Add(new PlayingCard(options, PlayingCardSuit.Red, PlayingCardRank.Joker));
            }
        }

        protected PlayingCardOptions CardOptions
        {
            get {
                return (PlayingCardOptions)Options;
            }
        }
    }
}

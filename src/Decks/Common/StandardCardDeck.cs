using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Common
{
    [JsonConverter(typeof(Internal.Serialization.DeckSerializer<PlayingCard>))]
    public class StandardCardDeck : Deck<PlayingCard>
    {
        public StandardCardDeck(PlayingCardOptions options, bool doInitialize = true): base(options, doInitialize)
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

        public override void Rehydrated()
        {
            base.Rehydrated();
            DrawPile.Apply(c => c.Options = CardOptions);
            DiscardPile.Apply(c => c.Options = CardOptions);
            Table.Apply(c => c.Options = CardOptions);
            Tableau.Apply(c => c.Options = CardOptions);
            Hands.Apply(h => h.Apply(c => c.Options = CardOptions));
        }

        protected PlayingCardOptions CardOptions
        {
            get {
                return (PlayingCardOptions)Options;
            }
        }
    }
}

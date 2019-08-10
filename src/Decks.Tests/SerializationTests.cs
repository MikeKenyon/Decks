using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Decks.Common;
using Decks.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Newtonsoft.Json;

namespace Decks.Tests
{
    [TestClass]
    public class SerializationTests
    {
        [TestMethod]
        public void GenericSerialization()
        {
            // Arrange
            var deck = new Deck<string>(MakeOptions());
            deck.Add("This")
                .Add("is")
                .Add("a")
                .Add("sample")
                .Add("deck")
                .Add("that")
                .Add("contains")
                .Add("several")
                .Add("strings")
                .Add("representing")
                .Add("the")
                .Add("card")
                .Add("elements.")
                .Add("There")
                .Add("need")
                .Add("to")
                .Add("be")
                .Add("enough")
                .Add("blocks")
                .Add("of")
                .Add("text")
                .Add("fascilitating")
                .Add("drawing")
                .Add("up")
                .Add("five")
                .Add("hands")
                .Add("and")
                .Add("tableau")
                .Add("space")
                ;
            PutThroughPaces(deck);
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            // Act
            var text = JsonConvert.SerializeObject(deck, settings);
            var created = JsonConvert.DeserializeObject<Deck<string>>(text);
            // Assert
        }
        [TestMethod]
        public void StandardDeckSerialization()
        {
            // Arrange
            var deck = new StandardCardDeck(MakeCardOptions());
            PutThroughPaces(deck);
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            // Act
            var text = JsonConvert.SerializeObject(deck, settings);
            var created = JsonConvert.DeserializeObject<StandardCardDeck>(text);
            // Assert
        }

        private static void PutThroughPaces<TElement>(IDeck<TElement> deck) where TElement : class
        {
            deck.DrawPile.Shuffle();
            var hands = deck.Deal(5);
            deck.DrawPile.Shuffle();

            var hand = hands.ElementAt(1);
            hand.Play(hand.ElementAt(2));
            hand.Play(hand.ElementAt(2));

            hand = hands.ElementAt(2);
            hand.Muck();

            deck.Tableau.DrawUp();
        }

        private DeckOptions MakeOptions()
        {
            return new DeckOptions
            {
                Discards = { AutoShuffle = false },
                DrawPile = { MaximumShuffleCount = null },
                Hands = { InitialHandSize = 5, Enabled = true },
                Modifiable = true,
                Table = { Enabled = true },
                Tableau = {
                    Enabled = true,
                    CanDrawIntoHand = true, CanPlayToTable = true, DrawsUpSafely = true,
                    InitialSize = 3, MaintainSize = false, MaximumSize = 3,
                    OverflowRule = TableauOverflowRule.DiscardRandom
                },
            };
        }
        private PlayingCardOptions MakeCardOptions()
        {
            return new PlayingCardOptions
            {
                AceMode = AceMode.High,
                Discards = { AutoShuffle = false },
                DrawPile = { MaximumShuffleCount = null },
                Hands = { InitialHandSize = 5, Enabled = true },
                HasJokers = false,
                Modifiable = false,
                Order = PlayingCardSortOrder.BridgeOrder,
                Table = { Enabled = true },
                Tableau = {
                    Enabled = true,
                    CanDrawIntoHand = true, CanPlayToTable = true, DrawsUpSafely = true,
                    InitialSize = 3, MaintainSize = false, MaximumSize = 3,
                    OverflowRule = TableauOverflowRule.DiscardRandom
                },
            };
        }
    }
}

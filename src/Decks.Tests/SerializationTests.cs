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
        public void StandardDeckSerialization()
        {
            // Arrange
            var deck = new StandardCardDeck(MakeOptions());
            PutThroughPaces(deck);
            var settings = new JsonSerializerSettings
            {
                Formatting = Formatting.Indented
            };
            // Act
            var text = JsonConvert.SerializeObject(deck, settings);
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

        private PlayingCardOptions MakeOptions()
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

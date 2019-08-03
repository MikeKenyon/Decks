using System;
using System.Collections.Generic;
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
            var hands = deck.Deal(5);
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
                Formatting = Formatting.Indented
            };
            // Act
            var text = JsonConvert.SerializeObject(deck, settings);
            var found = JsonConvert.DeserializeObject<StandardCardDeck>(text);
            // Assert
            Assert.AreEqual(deck.Options.Hands.InitialHandSize, found.Options.Hands.InitialHandSize);
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

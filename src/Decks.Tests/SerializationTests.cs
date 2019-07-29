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
            var deck = new StandardCardDeck(new PlayingCardOptions
            {
                Hands = new HandOptions { InitialHandSize = 5 },
            });
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
    }
}

using System;
using System.Collections.Generic;
using System.Text;

using Decks.Common;

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
                HandSize = 5,
            });
            var hands = deck.Deal(5);
            // Act
            var text = JsonConvert.SerializeObject(deck);
            // Assert
        }
    }
}

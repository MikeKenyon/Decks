using Decks.Common;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Tests
{
    [TestClass]
    public class PinochleDeckTests
    {
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void LowAceDoesntWork()
        {
            // Arrange
            var options = new PinochleOptions
            {
                AceMode = AceMode.Low
            };
            //Act
            var deck = new PinochleDeck(options);
        }

        public void HasJokersDoesntWork()
        {
            // Arrange
            var options = new PinochleOptions
            {
                HasJokers = true,
            };
            //Act
            var deck = new PinochleDeck(options);
        }

        [TestMethod]
        public void BasicCreationTest()
        {
            // Arrange
            var options = new PinochleOptions();
            // Act
            var deck = new PinochleDeck(options);
            // Assert
            Assert.AreEqual(48, deck.DrawPile.Count);
        }
    }
}

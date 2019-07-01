using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using Decks;
using System.Linq;
using Decks.Configuration;

namespace Decks.Tests
{
    [TestClass]
    public class TableauTests
    {
        [TestMethod]
        public void NoTableauByOptions()
        {
            // Arrange
            var options = new DeckOptions { TableauSize = 0 };
            var deck = new Deck<string>(options);
            // Act & Assert
            var enabled = deck.Tableau.Enabled;
            Assert.IsFalse(enabled);
        }
        [TestMethod]
        public void TableauByOptions()
        {
            // Arrange
            var options = new DeckOptions { TableauSize = 3 };
            var deck = new Deck<string>(options);
            // Act & Assert
            var enabled = deck.Tableau.Enabled;
            Assert.IsTrue(enabled);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayToHandRequiresPermissions()
        {
            // Arrange
            var options = new DeckOptions
            {
                TableauSize = 1,
                HandSize = 1,
                Allow = ValidOperations.Add | ValidOperations.DealMuck |
                    ValidOperations.PlayTableauToTable
            };
            var deck = new Deck<string>(options)
                .Add("Thing");
            deck.Tableau.DrawUp();
            var hand = deck.Deal(1, 0).First();
            // Act & Assert
            deck.Tableau.DrawInto(deck.Tableau.First(), hand);
        }

        [TestMethod]
        [ExpectedException(typeof(BottomDeckException))]
        public void DefaultTableauCanBottomDeck()
        {
            // Arrange
            var options = new DeckOptions
            {
                TableauSize = 3,
            };
            var deck = new Deck<string>(options)
                .Add("Thing");
            // Act & Assert
            deck.Tableau.DrawUp();
        }

        [TestMethod]
        public void AlteredTableauCannotBottomDeck()
        {
            // Arrange
            var options = new DeckOptions
            {
                TableauSize = 3,
                TableauDrawsUpSafely = true
            };
            var deck = new Deck<string>(options)
                .Add("Thing");
            // Act & Assert
            deck.Tableau.DrawUp();
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void PlayToTableRequiresPermissions()
        {
            // Arrange
            var options = new DeckOptions {
                TableauSize = 1,
                Allow = ValidOperations.Add | ValidOperations.PlayTableauToHand
            };
            var deck = new Deck<string>(options)
                .Add("Thing");
            deck.Tableau.DrawUp();
            // Act & Assert
            deck.Tableau.Play(deck.Tableau.First());
        }

        [TestMethod]
        public void TableauAddToDrawFrom()
        {
            // Arrange
            var options = new DeckOptions {
                TableauSize = 3,
                TableauMaintainSize = true,
                HandSize = 2
            };
            var deck = new Deck<string>(options);
            deck.Add("This")
                .Add("is")
                .Add("a", Location.Tableau)
                .Add("deck")
                .Add("with")
                .Add("the")
                .Add("tableau");
            // Act & Assert
            Assert.AreEqual(1, deck.Tableau.Count);
            Assert.AreEqual(6, deck.Count);

            deck.Tableau.DrawUp();
            Assert.AreEqual(3, deck.Tableau.Count);
            Assert.AreEqual(4, deck.Count);
            var wasLast = deck.Tableau.Last();

            deck.Tableau.Play(deck.Tableau.ElementAt(1));
            Assert.AreEqual(3, deck.Tableau.Count);
            Assert.AreEqual(3, deck.Count);
            Assert.AreEqual(wasLast, deck.Tableau.ElementAt(1));

            var hand = deck.Deal(1, 1).First();
            Assert.AreEqual(3, deck.Tableau.Count);
            Assert.AreEqual(2, deck.Count);
            Assert.AreEqual(1, deck.Hands.Count);
            Assert.AreEqual(1, hand.Count);

            deck.Tableau.DrawInto(deck.Tableau.ElementAt(1), hand);
            Assert.AreEqual(3, deck.Tableau.Count);
            Assert.AreEqual(1, deck.Count);
            Assert.AreEqual(1, deck.Hands.Count);
            Assert.AreEqual(2, hand.Count);
        }
    }
}

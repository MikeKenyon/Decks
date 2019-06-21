using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decks.Tests
{
    [TestClass]
    public class HandTests
    {
        [TestMethod]
        public void DrawToHand()
        {
            // Arrange
            var options = new DeckOptions() { HandSize = 0 };
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.Add("is");
            deck.Add("a");
            deck.Add("sample");
            deck.Add("deck.");
            var hand = deck.Deal();
            hand.Draw();
            // Assert
            Assert.AreEqual(4, deck.Count);
            Assert.AreEqual(0, deck.TableCount);
            Assert.AreEqual(0, deck.DiscardCount);
            Assert.AreEqual(5, deck.TotalCount);

            Assert.AreEqual(1, hand.Count);
            Assert.AreEqual("deck.", hand.First());
        }

        [TestMethod]
        public void MuckFromHand()
        {
            // Arrange
            var options = new DeckOptions() { HandSize = 3 };
            var deck = new Deck<string>(options);
            // Act 1
            deck.Add("This");
            deck.Add("is");
            deck.Add("a");
            deck.Add("sample");
            deck.Add("deck.");
            var hand = deck.Deal();
            // Assert 1
            Assert.AreEqual(3, hand.Count);
            Assert.AreEqual(5, deck.TotalCount);
            Assert.AreEqual(2, deck.Count);
            Assert.AreEqual(0, deck.DiscardCount);
            // Act 2
            hand.Muck();
            // Assert 2
            Assert.AreEqual(0, hand.Count);
            Assert.IsTrue(hand.HasBeenMucked);
            Assert.AreEqual(5, deck.TotalCount);
            Assert.AreEqual(2, deck.Count);
            Assert.AreEqual(3, deck.DiscardCount);
        }
    }
}

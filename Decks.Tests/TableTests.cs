using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decks.Tests
{
    [TestClass]
    public class TableTests
    {
        [TestMethod]
        public void DiscardFromPlay()
        {
            // Arrange
            var options = new DeckOptions() { HandSize = 0 };
            var deck = new Deck<string>(options);
            deck.Add("This");
            deck.Add("is");
            deck.Add("a");
            deck.Add("sample");
            deck.Add("deck.");
            var hand = deck.Deal();
            hand.Draw();
            hand.Play(hand.First());
            // Act
            deck.Table.Discard(deck.Table.First());
            // Assert
            Assert.AreEqual(0, hand.Count);
            Assert.AreEqual(1, deck.DiscardCount);
        }
        [TestMethod]
        public void MuckPlay()
        {
            // Arrange
            var options = new DeckOptions() { HandSize = 0 };
            var deck = new Deck<string>(options);
            deck.Add("This");
            deck.Add("is");
            deck.Add("a");
            deck.Add("sample");
            deck.Add("deck.");
            var hand = deck.Deal();
            hand.Draw();
            hand.Draw();
            hand.Play(hand.First());
            hand.Play(hand.First());
            // Act
            deck.Table.Muck();
            // Assert
            Assert.AreEqual(0, hand.Count);
            Assert.AreEqual(2, deck.DiscardCount);
        }
    }
}

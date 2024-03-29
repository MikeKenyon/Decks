﻿using System;
using Decks.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Decks.Tests
{
    [TestClass]
    public class SimpleDeckTests
    {
        [TestMethod]
        public void CreateIntoTopDeckTests()
        {
            // Arrange
            var options = new DeckOptions();
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.Add("is");
            deck.Add("a");
            deck.Add("sample");
            deck.Add("deck.");
            deck.DrawPile.Shuffle();
            // Assert
            Assert.AreEqual(5, deck.DrawPile.Count);
            Assert.AreEqual(0, deck.Table.Count);
            Assert.AreEqual(0, deck.DiscardPile.Count);
            Assert.AreEqual(5, deck.Count);
        }
        [TestMethod]
        public void CreateIntoAllLocationsTests()
        {
            // Arrange
            var options = new DeckOptions() {
                Hands = new HandOptions { InitialHandSize = 0 },
                Table = new TableOptions { Enabled = true },
                Tableau = new TableauOptions { Enabled = true },
            };
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.Add("is", Location.DiscardPile);
            deck.Add("a", Location.Table);
            deck.Add("sample");
            deck.Add("string", Location.Tableau);
            deck.Add("deck.", Location.DiscardPile);
            deck.DrawPile.Shuffle(false);
            // Assert
            Assert.AreEqual(2, deck.DrawPile.Count);
            Assert.AreEqual(1, deck.Table.Count);
            Assert.AreEqual(2, deck.DiscardPile.Count);
            Assert.AreEqual(1, deck.Tableau.Count);
            Assert.AreEqual(6, deck.Count);
        }
        [TestMethod]
        public void ContainsElements()
        {
            // Arrange
            var options = new DeckOptions() {
                Hands = new HandOptions { InitialHandSize = 1 },
                Table = new TableOptions { Enabled = true } };
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.Add("is", Location.DiscardPile);
            deck.Add("a", Location.Table);
            deck.Add("sample");
            deck.Add("deck.", Location.DiscardPile);
            var hand = deck.Deal();
            // Act
            // Assert
            Assert.IsTrue(deck.Contains("is", Location.DiscardPile));
            Assert.IsFalse(deck.Contains("is", Location.DrawPile));
            Assert.IsTrue(deck.Contains("This"));
            Assert.IsTrue(deck.Contains("a", Location.Table));
            Assert.IsTrue(deck.Contains("sample", Location.Hand));
            Assert.IsTrue(hand.Contains("sample"));
        }
        [TestMethod]
        public void ShuffleWithRetreival()
        {
            // Arrange
            var options = new DeckOptions() { Table = new TableOptions { Enabled = true } };
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.Add("is", Location.DiscardPile);
            deck.Add("a", Location.Table);
            deck.Add("sample");
            deck.Add("deck.", Location.DiscardPile);
            deck.DrawPile.Shuffle();
            // Assert
            Assert.AreEqual(4, deck.DrawPile.Count);
            Assert.AreEqual(1, deck.Table.Count);
            Assert.AreEqual(0, deck.DiscardPile.Count);
            Assert.AreEqual(5, deck.Count);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShuffleFailsWithoutOnceAllowed()
        {
            // Arrange
            var options = new DeckOptions() { DrawPile = new DrawPileOptions { MaximumShuffleCount = 0 } };
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.DrawPile.Shuffle();
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShuffleFailsWithoutResuffleAllowed() 
        {
            // Arrange
            var options = new DeckOptions() { DrawPile = new DrawPileOptions { MaximumShuffleCount = 1 } };
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.DrawPile.Shuffle();
            deck.DrawPile.Shuffle();
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddToHandFails()
        {
            // Arrange
            var options = new DeckOptions();
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This", Location.Hand);
        }
    }
}

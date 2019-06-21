using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

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
            deck.Shuffle();
            // Assert
            Assert.AreEqual(5, deck.Count);
            Assert.AreEqual(0, deck.TableCount);
            Assert.AreEqual(0, deck.DiscardCount);
            Assert.AreEqual(5, deck.TotalCount);
        }
        [TestMethod]
        public void CreateIntoAllLocationsTests()
        {
            // Arrange
            var options = new DeckOptions();
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.Add("is", Location.DiscardPile);
            deck.Add("a", Location.Table);
            deck.Add("sample");
            deck.Add("deck.", Location.DiscardPile);
            deck.Shuffle(false);
            // Assert
            Assert.AreEqual(2, deck.Count);
            Assert.AreEqual(1, deck.TableCount);
            Assert.AreEqual(2, deck.DiscardCount);
            Assert.AreEqual(5, deck.TotalCount);
        }
        [TestMethod]
        public void ContainsElements()
        {
            // Arrange
            var options = new DeckOptions() { HandSize = 1 };
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
            Assert.IsFalse(deck.Contains("is", Location.TopDeck));
            Assert.IsTrue(deck.Contains("This"));
            Assert.IsTrue(deck.Contains("a", Location.Table));
            Assert.IsTrue(deck.Contains("sample", Location.Hand));
            Assert.IsTrue(hand.Contains("sample"));
        }
        [TestMethod]
        public void ShuffleWithRetreival()
        {
            // Arrange
            var options = new DeckOptions();
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.Add("is", Location.DiscardPile);
            deck.Add("a", Location.Table);
            deck.Add("sample");
            deck.Add("deck.", Location.DiscardPile);
            deck.Shuffle();
            // Assert
            Assert.AreEqual(4, deck.Count);
            Assert.AreEqual(1, deck.TableCount);
            Assert.AreEqual(0, deck.DiscardCount);
            Assert.AreEqual(5, deck.TotalCount);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShuffleFailsWithoutOnceAllowed()
        {
            // Arrange
            var options = new DeckOptions() { Allow = ValidOperations.None };
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.Shuffle();
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ShuffleFailsWithoutResuffleAllowed() 
        {
            // Arrange
            var options = new DeckOptions() { Allow = ValidOperations.ShuffleOnce };
            var deck = new Deck<string>(options);
            // Act
            deck.Add("This");
            deck.Shuffle();
            deck.Shuffle();
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

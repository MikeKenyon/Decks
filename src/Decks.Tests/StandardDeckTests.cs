using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Decks.Common;
using Decks.Configuration;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Decks.Tests
{
    [TestClass]
    public class StandardDeckTests
    {
        [TestMethod]
        public void CreateStandardDeck()
        {
            // Arrange
            // Act
            var deck = new StandardCardDeck(new PlayingCardOptions
            {
                AceMode = AceMode.High,
                Order = PlayingCardSortOrder.PokerOrder,
                Hands = new HandOptions { InitialHandSize = 5 },
                Discards = new DiscardOptions { AutoShuffle = true },
                HasJokers = false
            });
            // Assert
            Assert.AreEqual(52, deck.Count);
            Assert.AreEqual(0, deck.DiscardPile.Count);
        }

        [TestMethod]
        public void DealTexasHands()
        {
            // Arrange
            var deck = new StandardCardDeck(new PlayingCardOptions
            {
                AceMode = AceMode.High,
                Order = PlayingCardSortOrder.PokerOrder,
                Hands = new HandOptions { InitialHandSize = 2 },
                Discards = new DiscardOptions { AutoShuffle = true },
                HasJokers = false
            });
            // Act
            // Assert
            deck.DrawPile.Shuffle();
            var hands = deck.Deal(10, 2);
            Assert.AreEqual(10, hands.Count());
            foreach(var hand in hands)
            {
                Assert.AreEqual(2, hand.Count);
            }
            Assert.AreEqual(32, deck.Count);
            Assert.AreEqual(0, deck.DiscardPile.Count);
            Assert.AreEqual(0, deck.Table.Count);
            Assert.AreEqual(52, deck.TotalCount);

            deck.Muck(hands.ElementAt(3));
            Assert.AreEqual(9, deck.Hands.Count);
            Assert.AreEqual(2, deck.DiscardPile.Count);
            Assert.AreEqual(32, deck.Count);
            Assert.AreEqual(52, deck.TotalCount);

            deck.Muck();
            Assert.AreEqual(0, deck.Hands.Count);
            Assert.AreEqual(20, deck.DiscardPile.Count);
            Assert.AreEqual(32, deck.Count);
            Assert.AreEqual(52, deck.TotalCount);
        }
    }
}

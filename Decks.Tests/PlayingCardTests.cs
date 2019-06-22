using System;
using System.Collections.Generic;
using System.Text;

using Decks.Common;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Decks.Tests
{
    [TestClass]
    public class PlayingCardTests
    {
        [TestMethod]
        public void PlayingCardToString()
        {
            // Arrange
            var options = new PlayingCardOptions();
            var pc1 = new PlayingCard(options, PlayingCardSuit.Clubs, PlayingCardRank.Seven);
            var pc2 = new PlayingCard(options, PlayingCardSuit.Red, PlayingCardRank.Joker);
            var pc3 = new PlayingCard(options, PlayingCardSuit.Spades, PlayingCardRank.Ace);
            var pc4 = new PlayingCard(options, PlayingCardSuit.Diamonds, PlayingCardRank.Queen);
            var pc5 = new PlayingCard(options, PlayingCardSuit.Hearts, PlayingCardRank.Ten);

            // Act
            var t1 = pc1.ToString();
            var t2 = pc2.ToString();
            var t3 = pc3.ToString();
            var t4 = pc4.ToString();
            var t5 = pc5.ToString();

            // Assert
            Assert.AreEqual("7♣", t1);
            Assert.AreEqual("Red Joker", t2);
            Assert.AreEqual("A♠️", t3);
            Assert.AreEqual("Q♦", t4);
            Assert.AreEqual("10♥", t5);
        }

        [TestMethod]
        public void RedBlackTests()
        {
            // Arrange
            var options = new PlayingCardOptions();
            var pc1 = new PlayingCard(options, PlayingCardSuit.Clubs, PlayingCardRank.Seven);
            var pc2 = new PlayingCard(options, PlayingCardSuit.Red, PlayingCardRank.Joker);
            var pc3 = new PlayingCard(options, PlayingCardSuit.Spades, PlayingCardRank.Ace);
            var pc4 = new PlayingCard(options, PlayingCardSuit.Diamonds, PlayingCardRank.Queen);
            var pc5 = new PlayingCard(options, PlayingCardSuit.Hearts, PlayingCardRank.Ten);

            // Act
            var t1 = pc1.IsBlack;
            var t2 = pc2.IsBlack;
            var t3 = pc3.IsBlack;
            var t4 = pc4.IsBlack;
            var t5 = pc5.IsBlack;

            // Assert
            Assert.IsTrue(t1);
            Assert.IsFalse(t2);
            Assert.IsTrue(t3);
            Assert.IsFalse(t4);
            Assert.IsFalse(t5);
        }

        [TestMethod]
        public void PokerOrderTest()
        {
            // Arrange
            var options = new PlayingCardOptions() { AceMode = AceMode.High, Order = PlayingCardSortOrder.PokerOrder };
            var pc1 = new PlayingCard(options, PlayingCardSuit.Clubs, PlayingCardRank.Seven);
            var pc2 = new PlayingCard(options, PlayingCardSuit.Red, PlayingCardRank.Joker);
            var pc3 = new PlayingCard(options, PlayingCardSuit.Spades, PlayingCardRank.Ace);
            var pc4 = new PlayingCard(options, PlayingCardSuit.Diamonds, PlayingCardRank.Queen);
            var pc5 = new PlayingCard(options, PlayingCardSuit.Hearts, PlayingCardRank.Seven);

            // Act
            var t1 = pc1.CompareTo(pc4);
            var t2 = pc1.CompareTo(pc1);
            var t3 = pc3.CompareTo(pc4);
            var t4 = pc2.CompareTo(pc4);
            var t5 = pc1.CompareTo(pc5);

            // Assert
            Assert.IsTrue(t1 < 0);
            Assert.IsTrue(t2 == 0);
            Assert.IsTrue(t3 > 0);
            Assert.IsTrue(t4 > 0);
            Assert.IsTrue(t5 > 0);
        }
    }
}

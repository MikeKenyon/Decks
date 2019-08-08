using Newtonsoft.Json;
using System;

namespace Decks.Common
{
    /// <summary>
    /// A standard playing card.  
    /// </summary>
    [JsonConverter(typeof(Internal.Serialization.PlayingCardSerializer))]
    public class PlayingCard : IComparable<PlayingCard>, IEquatable<PlayingCard>
    {
        /// <summary>
        /// Creates a playing card.
        /// </summary>
        /// <param name="options">The options on how cards are played.</param>
        /// <param name="suit">The suit of the card.</param>
        /// <param name="rank">The rank of the card.</param>
        internal PlayingCard(PlayingCardOptions options, PlayingCardSuit suit, PlayingCardRank rank)
        {
            Rank = rank;
            Suit = suit;
            Options = options;
        }
        /// <summary>
        /// The rank of the card.
        /// </summary>
        public PlayingCardRank Rank { get; private set; }
        /// <summary>
        /// The suit of the card.
        /// </summary>
        public PlayingCardSuit Suit { get; private set; }
        /// <summary>
        /// The options applicable to this card.
        /// </summary>
        internal PlayingCardOptions Options { get; set; }

        /// <summary>
        /// Checks if this card is red or not.
        /// </summary>
        public bool IsRed
        {
            get
            {
                switch (Suit)
                {
                    case PlayingCardSuit.Diamonds:
                    case PlayingCardSuit.Hearts:
                    case PlayingCardSuit.Red:
                        return true;
                    default:
                        return false;
                }
            }
        }
        /// <summary>
        /// Checks if this card is black or not.
        /// </summary>
        public bool IsBlack { get { return !IsRed; } }
        /// <summary>
        /// Compares this card to a comprable card.
        /// </summary>
        /// <param name="other"></param>
        /// <returns></returns>
        public int CompareTo(PlayingCard other)
        {
            PlayingCardSuit[] suits = null;
            switch (Options.Order)
            {
                case PlayingCardSortOrder.BoxOrder:
                    suits = new[] {
                        PlayingCardSuit.Spades,
                        PlayingCardSuit.Diamonds,
                        PlayingCardSuit.Clubs,
                        PlayingCardSuit.Hearts,
                        PlayingCardSuit.Red,
                        PlayingCardSuit.Black,
                    };
                    break;
                case PlayingCardSortOrder.BridgeOrder:
                    suits = new[] {
                        PlayingCardSuit.Black,
                        PlayingCardSuit.Red,
                        PlayingCardSuit.Spades,
                        PlayingCardSuit.Hearts,
                        PlayingCardSuit.Diamonds,
                        PlayingCardSuit.Clubs,
                    };
                    break;
                case PlayingCardSortOrder.PokerOrder:
                    suits = new[] {
                        PlayingCardSuit.Black,
                        PlayingCardSuit.Red,
                        PlayingCardSuit.Spades,
                        PlayingCardSuit.Hearts,
                        PlayingCardSuit.Clubs,
                        PlayingCardSuit.Diamonds,
                    };
                    break;
                default:
                    throw new NotImplementedException($"The value of {Options.Order} wasn't coded for.");
            }
            var compare = 0;
            if (Rank == PlayingCardRank.Ace || other.Rank == PlayingCardRank.Ace)
            {
                if (Rank == other.Rank)
                {
                    compare = 0;
                }
                else
                {
                    switch (Options.AceMode)
                    {
                        case AceMode.High:
                        case AceMode.HighLow:
                            compare = 1;
                            break;
                        case AceMode.Low:
                            compare = -1;
                            break;
                        default:
                            throw new NotImplementedException($"The value of {Options.AceMode} wasn't coded for.");
                    }
                }
            }
            else
            {
                compare = Rank.CompareTo(other.Rank);
            }
            if (compare == 0)
            {
                compare = Array.IndexOf(suits, Suit)
                            .CompareTo(Array.IndexOf(suits, other.Suit));
            }
            return compare;
        }

        /// <summary>
        /// Compares this card to another.
        /// </summary>
        /// <param name="other">The card to compare ourselves to.</param>
        /// <returns>Standard comparison value - negative is less than, 0 is equal, positive is greater than.</returns>
        public bool Equals(PlayingCard other)
        {
            return CompareTo(other) == 0;
        }
        /// <summary>
        /// Compares this card to another.
        /// </summary>
        /// <param name="obj">The card to compare ourselves to.</param>
        /// <returns>Standard comparison value - negative is less than, 0 is equal, positive is greater than.</returns>
        public override bool Equals(object obj)
        {
            return obj is PlayingCard pc && ((IEquatable<PlayingCard>)this).Equals(pc);
        }
        /// <summary>
        /// Gets the hash code for this card.
        /// </summary>
        /// <returns>A hash code for this card.</returns>
        public override int GetHashCode()
        {
            return (int)Rank * ((int)Suit * 13);
        }

        /// <summary>
        /// Returns a string representation of this card.
        /// </summary>
        /// <returns>The string version of this card.</returns>
        public override string ToString()
        {
            var suit = Suit.ToString();
            var rank = ((int)Rank).ToString();
            switch (Suit)
            {
                case PlayingCardSuit.Clubs:
                    suit = "♣";
                    break;
                case PlayingCardSuit.Diamonds:
                    suit = "♦";
                    break;
                case PlayingCardSuit.Hearts:
                    suit = "♥";
                    break;
                case PlayingCardSuit.Spades:
                    suit = "♠️";
                    break;
            }
            switch (Rank)
            {
                case PlayingCardRank.Ace:
                    rank = "A";
                    break;
                case PlayingCardRank.Jack:
                    rank = "J";
                    break;
                case PlayingCardRank.Queen:
                    rank = "Q";
                    break;
                case PlayingCardRank.King:
                    rank = "K";
                    break;
                case PlayingCardRank.Joker:
                    rank = Rank.ToString();
                    break;
                default:
                    break;
            }
            return $"{suit} {rank}";
        }

        /// <summary>
        /// Parses a string into a playing card.
        /// </summary>
        /// <param name="text">The text to parse.</param>
        /// <returns>The parsed result.</returns>
        /// <exception cref="FormatException">If the <paramref name="text"/> isn't a playing card string.</exception>
        public static PlayingCard Parse(string text)
        {
            if (!TryParse(text, out var card))
            {
                throw new FormatException($"'{text}' is not a well-formatted playing card description.");
            }
            return card;
        }
        /// <summary>
        /// Tries to parse text into a string.
        /// </summary>
        /// <param name="text">The text to try and parse.</param>
        /// <param name="card">The resulting card.</param>
        /// <returns><see langword="true"/> if the parsing was successful.</returns>
        public static bool TryParse(string text, out PlayingCard card)
        {
            var good = false;
            var parts = text.Split(' ');

            card = null;

            if (parts.Length == 2)
            {
                var suit = parts[0];
                var rank = parts[1];
                var foundSuit = PlayingCardSuit.Black;
                var foundRank = PlayingCardRank.Ace;

                switch (suit)
                {
                    case "♣":
                        foundSuit = PlayingCardSuit.Clubs;
                        break;
                    case "♦":
                        foundSuit = PlayingCardSuit.Diamonds;
                        break;
                    case "♥":
                        foundSuit = PlayingCardSuit.Hearts;
                        break;
                    case "♠️":
                        foundSuit = PlayingCardSuit.Spades;
                        break;
                    default:
                        foundSuit = (PlayingCardSuit)
                            Enum.Parse(typeof(PlayingCardSuit), suit, true);
                        break;
                }
                switch (rank)
                {
                    case "A": foundRank = PlayingCardRank.Ace; break;
                    case "J": foundRank = PlayingCardRank.Jack; break;
                    case "Q": foundRank = PlayingCardRank.Queen; break;
                    case "K": foundRank = PlayingCardRank.King; break;
                    case "Joker": foundRank = PlayingCardRank.Joker; break;
                    default:
                        foundRank = (PlayingCardRank)int.Parse(rank);
                        break;
                }
                card = new PlayingCard(null, foundSuit, foundRank);
                good = true;
            }


            return good;
        }
    }
}

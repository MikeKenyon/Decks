using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decks.Common
{
    [JsonConverter(typeof(Internal.Serialization.PlayingCardSerializer))]
    public class PlayingCard : IComparable<PlayingCard>, IEquatable<PlayingCard>
    {
        internal PlayingCard(PlayingCardOptions options, PlayingCardSuit suit, PlayingCardRank rank)
        {
            Rank = rank;
            Suit = suit;
            Options = options;
        }
        public PlayingCardRank Rank { get; private set; }
        public PlayingCardSuit Suit { get; private set; }
        internal PlayingCardOptions Options { get; set; }

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
        public bool IsBlack { get { return !IsRed; } }
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
                    }
                }
            }
            else
            {
                compare = Rank.CompareTo(other.Rank);
            }
            if(compare == 0)
            {
                compare = Array.IndexOf(suits, this.Suit)
                            .CompareTo(Array.IndexOf(suits, other.Suit));
            }
            return compare;
        }

        public bool Equals(PlayingCard other)
        {
            return this.CompareTo(other) == 0;
        }
        public override bool Equals(object obj)
        {
            return obj is PlayingCard pc && ((IEquatable<PlayingCard>)this).Equals(pc);
        }
        public override int GetHashCode()
        {
            return (int)Rank * ((int)Suit * 13);
        }

        public override string ToString()
        {
            var suit = Suit.ToString();
            var rank = ((int)Rank).ToString();
            switch(Suit)
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
            switch(Rank)
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

        public static PlayingCard Parse(string text)
        {
            if(!TryParse(text, out var card))
            {
                throw new FormatException($"'{text}' is not a well-formatted playing card description.");
            }
            return card;
        }
        public static bool TryParse(string text, out PlayingCard card)
        {
            var good = false;
            var parts = text.Split(' ');

            card = null;

            if(parts.Length == 2)
            {
                var suit = parts[0];
                var rank = parts[1];
                var foundSuit = PlayingCardSuit.Black;
                var foundRank = PlayingCardRank.Ace;

                switch(suit)
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
                switch(rank)
                {
                    case "A": foundRank = PlayingCardRank.Ace; break;
                    case "J": foundRank = PlayingCardRank.Jack; break;
                    case "Q": foundRank = PlayingCardRank.Queen; break;
                    case "K": foundRank = PlayingCardRank.King; break;
                    case "Joker": foundRank = PlayingCardRank.Joker; break;
                    default:
                        foundRank = (PlayingCardRank)Int32.Parse(rank);
                        break;
                }
                card = new PlayingCard(null, foundSuit, foundRank);
                good = true;
            }


            return good;
        }
    }
}

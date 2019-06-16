using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decks.Common
{
    public class PlayingCard : IComparable<PlayingCard>, IEquatable<PlayingCard>
    {
        internal PlayingCard(PlayingCardOptions options, PlayingCardSuit suit, PlayingCardRank rank)
        {
            Rank = rank;
            Suit = suit;
            Options = options;
        }
        public PlayingCardRank Rank { get; }
        public PlayingCardSuit Suit { get; }
        private PlayingCardOptions Options { get; }

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
            if(Rank == PlayingCardRank.Joker)
            {
                return $"{Suit} {Rank}";
            }
            else
            {
                var suit = string.Empty;
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
                var rank = string.Empty;
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
                    default:
                        rank = ((int)Rank).ToString();
                        break;
                }
                return $"{rank}{suit}";
            }
        }
    }
}

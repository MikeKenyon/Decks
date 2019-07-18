using Decks.Configuration;
using Decks.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Decks
{
    /// <summary>
    /// The pile that people draw from.
    /// </summary>
    /// <typeparam name="TElement">The elements in the draw pile.</typeparam>
    internal class DrawPile<TElement> : DeckStack<TElement>, IDrawPile<TElement>, Internal.IDrawPileInternal<TElement> where TElement : class
    {
        /// <summary>
        /// Constructor for the draw pile.
        /// </summary>
        /// <param name="deck"></param>
        internal DrawPile(Deck<TElement> deck) : base(deck)
        {
        }
        /// <summary>
        /// The options related to the draw pile.
        /// </summary>
        public Configuration.IDrawPileOptions Options { get { return Deck.Options.DrawPile; } }
        /// <summary>
        /// Optionally gets the discards back and then randomly orders the cards.
        /// </summary>
        /// <param name="retreiveDiscards">Whether or not to clear out the discards.</param>
        public void Shuffle(bool retreiveDiscards = true)
        {
            CheckValidToShuffle();
            if (retreiveDiscards)
            {
                Deck.DiscardPileStack.Readd();
            }
            Contents.Shuffle();
            ++ShuffleCount;
        }

        /// <summary>
        /// Gets the number of times that a given topdeck has been shuffled.  Some games fix this number.
        /// </summary>
        public uint ShuffleCount { get; private set; }

        public bool HasBeenShuffled { get { return ShuffleCount > 0; } }

        private void CheckValidToShuffle()
        {
            if(Options.MaximumShuffleCount >= 0 && Options.MaximumShuffleCount <= ShuffleCount)
            {
                throw new InvalidOperationException($"Cannot shuffle the deck.  Already shuffled {ShuffleCount} of an allowed {Options.MaximumShuffleCount} times.");
            }
        }

        void IDrawPileInternal<TElement>.Add(TElement element, DeckSide side)
        {
            switch(side)
            {
                case DeckSide.Bottom:
                    Contents.Add(element);
                    break;
                case DeckSide.Default:
                case DeckSide.Top:
                    Contents.Insert(0, element);
                    break;
            }
        }

        TElement IDrawPileInternal<TElement>.Draw(DeckSide side)
        {
            if ((Count == 0 && Deck.DiscardPile.Count == 0) ||
                (Count == 0 && !Deck.Options.Discards.AutoShuffle))
            {
                throw new BottomDeckException();
            }
            else if (Count == 0) // Must be auto-shuffle with a discard.
            {
                Shuffle();
            }

            TElement card = null;
            var index = 0;
            switch (side)
            {
                case DeckSide.Bottom:
                    index = Contents.Count - 1;
                    break;
                case DeckSide.Default:
                case DeckSide.Top:
                    index = 0;
                    break;
            }

            card = Contents[index];
            Contents.RemoveAt(index);

            return card;
        }

        void IDeckStackInternal<TElement>.Add(TElement element)
        {
            ((IDrawPileInternal<TElement>)this).Add(element, DeckSide.Top);
        }

        void IDeckStackInternal<TElement>.CheckEnabled()
        {
            // No-op.
        }

    }
}

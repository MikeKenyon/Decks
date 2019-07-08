using Decks.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks
{
    internal class DrawPile<TElement> : DeckStack<TElement>, IDrawPile<TElement> where TElement : class
    {
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
                Contents.AddRange(Deck.DiscardPile);
                Deck.DiscardPileStack.Contents.Clear();
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


    }
}

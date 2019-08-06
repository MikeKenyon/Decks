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
        private uint _shuffleCount;
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
        /// <returns>Draw pile (for fluent purposes)</returns>
        public IDrawPile<TElement> Shuffle(bool retreiveDiscards = true)
        {
            CheckValidToShuffle();

            Deck.Events.Shuffling(_shuffleCount + 1);

            if (retreiveDiscards)
            {
                Deck.DiscardPileStack.Readd();
            }
            Contents.Shuffle();
            ++_shuffleCount;

            Deck.Events.Shuffled(_shuffleCount);

            return this;
        }

        /// <summary>
        /// Gets the number of times that a given topdeck has been shuffled.  Some games fix this number.
        /// </summary>
        uint IDrawPileInternal<TElement>.ShuffleCount {
            get { return _shuffleCount; }
            set { _shuffleCount = value; NotifyOfPropertyChange(); NotifyOfPropertyChange(() => HasBeenShuffled); }
        }

        /// <summary>
        /// Checks whether the deck has previously been shuffled.
        /// </summary>
        public bool HasBeenShuffled { get { return _shuffleCount > 0; } }

        /// <summary>
        /// Checks if it's okay to shuffle the deck.
        /// </summary>
        private void CheckValidToShuffle()
        {
            if(Options.MaximumShuffleCount.HasValue && Options.MaximumShuffleCount <= _shuffleCount)
            {
                throw new InvalidOperationException($"Cannot shuffle the deck.  Already shuffled {_shuffleCount} of an allowed {Options.MaximumShuffleCount} times.");
            }
        }

        /// <summary>
        /// Accepts a visitor to the draw pile.
        /// </summary>
        /// <param name="visitor">The visitor to accept.</param>
        void IDeckVisitable<TElement>.Accept(IDeckVisitor<TElement> visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Adds an element to the draw pile.
        /// </summary>
        /// <param name="element">The element to add.</param>
        /// <param name="side">Which side of the stack to add the element to.  Allows for putting cards on the bottom of the deck.</param>
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

        /// <summary>
        /// Draws a card from the deck.
        /// </summary>
        /// <param name="side">The side to draw from.  Allows for botom-dealing.</param>
        /// <returns>The drawn element.</returns>
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

            Deck.Events.Drawing(this);

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

            Deck.Events.Drew(this, card);

            return card;
        }

        /// <summary>
        /// Adds an element to the draw pile.
        /// </summary>
        /// <param name="element">The element to add.</param>
        void IDeckStackInternal<TElement>.Add(TElement element)
        {
            ((IDrawPileInternal<TElement>)this).Add(element, DeckSide.Default);
        }

        /// <summary>
        /// Confirms that this stack is enabled.
        /// </summary>
        void IDeckStackInternal<TElement>.CheckEnabled()
        {
            // No-op.
        }

    }
}

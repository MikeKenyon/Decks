using Decks.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;

namespace Decks
{
    /// <summary>
    /// An implementation of the <see cref="IHand{TElement}"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of the "card" in the hand.</typeparam>
    internal class Hand<TElement> : DeckStack<TElement>, IHand<TElement>, Internal.IHandInternal<TElement>
        where TElement : class
    {
        /// <summary>
        /// Creates a new hand.
        /// </summary>
        /// <param name="deck">The deck the hand is atttached to.</param>
        internal Hand(Deck<TElement> deck) : base(deck)
        {
        }
        private bool _hasBeenMucked;
        /// <summary>
        /// Determines if this hand has previously been mucked.
        /// </summary>
        public bool HasBeenMucked
        {
            get { return _hasBeenMucked; }
            internal set { _hasBeenMucked = value; NotifyOfPropertyChange(); }
        }

        /// <summary>
        /// Draws a card from the draw pile into the hand.
        /// </summary>
        /// <param name="from">Where to draw the card from, top or bottom of the deck.</param>
        /// <returns>This hand (for fluent purposes).</returns>
        public IHand<TElement> Draw(DeckSide side = DeckSide.Top)
        {
            Contract.Requires(Enum.IsDefined(typeof(DeckSide), side));

            Deck.Events.DrawingInto(this);

            var card = Deck.DrawPileStack.Draw(side);
            Contents.Add(card);

            Deck.Events.DrewInto(this, card);

            return this;
        }
        /// <summary>
        /// Mucks this hand. You should remove this reference after mucking the hand.
        /// </summary>
        public void Muck()
        {
            Deck.Events.Mucking(this);

            Contents.Apply(c => Deck.DiscardPileStack.Add(c));
            Contents.Clear();
            HasBeenMucked = true;
            Deck.RemoveHand(this);

            Deck.Events.Mucked(this);
        }
        /// <summary>
        /// Plays this element onto the table.
        /// </summary>
        /// <param name="element">The element to play.</param>
        /// <returns>This hand (for fluent purposes).</returns>
        /// <exception cref="InvalidElementException">
        /// The element isn't part of this hand.
        /// </exception>
        public IHand<TElement> Play(TElement element)
        {
            InvalidCheck();
            Deck.TableStack.CheckEnabled();
            if (!Contains(element))
            {
                throw new InvalidElementException("Element isn't part of this hand, cannot play it.");
            }

            Deck.Events.Playing(this, element);

            Contents.Remove(element);
            Deck.TableStack.Add(element);

            Deck.Events.Played(this, element);

            return this;
        }
        /// <summary>
        /// Checks to see if this hand contains a specific element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public override bool Contains(TElement element)
        {
            InvalidCheck();
            return base.Contains(element);
        }

        /// <summary>
        /// Gets the elements of the hand.
        /// </summary>
        /// <returns></returns>
        public override IEnumerator<TElement> GetEnumerator()
        {
            InvalidCheck();
            return base.GetEnumerator();
        }

        /// <summary>
        /// Checks to see if this hand has been mucked.
        /// </summary>
        private void InvalidCheck()
        {
            if (HasBeenMucked)
            {
                throw new ObjectDisposedException("Hand");
            }
        }

        /// <summary>
        /// Accepts a visitor to this hand.
        /// </summary>
        /// <param name="visitor">The visitor to accept.</param>
        void IDeckVisitable<TElement>.Accept(IDeckVisitor<TElement> visitor)
        {
            visitor.Visit(this);
        }

        /// <summary>
        /// Adds an element to this hand.
        /// </summary>
        /// <param name="element">An element in this hand.</param>
        void IDeckStackInternal<TElement>.Add(TElement element)
        {
            Contents.Add(element);
        }

        /// <summary>
        /// Confirms this hand is enabled.
        /// </summary>
        void IDeckStackInternal<TElement>.CheckEnabled()
        {
            // No-op
        }
    }
}

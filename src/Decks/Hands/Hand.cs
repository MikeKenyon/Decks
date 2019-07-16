using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Text;
using Decks.Internal;

namespace Decks
{
    /// <summary>
    /// An implementation of the <see cref="IHand{TElement}"/>.
    /// </summary>
    /// <typeparam name="TElement">The type of the "card" in the hand.</typeparam>
    internal class Hand<TElement> : DeckStack<TElement>, IHand<TElement>, Internal.IHandInternal<TElement>
        where TElement : class
    {
        internal Hand(Deck<TElement> deck) : base(deck)
        {
        }
        public bool HasBeenMucked { get; internal set; }

        public void Draw(DeckSide side = DeckSide.Top)
        {
            Contract.Requires(Enum.IsDefined(typeof(DeckSide), side));

            var card = Deck.DrawPileStack.Draw(side);
            Contents.Add(card);
        }
        public void Muck()
        {
            Contents.Apply(c => Deck.DiscardPileStack.Add(c));
            Contents.Clear();
            HasBeenMucked = true;
            Deck.RemoveHand(this);
        }
        /// <summary>
        /// Plays this element onto the table.
        /// </summary>
        /// <param name="element">The element to play.</param>
        /// <exception cref="InvalidElementException">
        /// The element isn't part of this hand.
        /// </exception>
        public void Play(TElement element)
        {
            InvalidCheck();
            Deck.TableStack.CheckEnabled();
            if(Contains(element))
            {
                Contents.Remove(element);
                Deck.TableStack.Add(element);
            }
            else
            {
                throw new InvalidElementException("Element isn't part of this hand, cannot play it.");
            }
        }

        public override bool Contains(TElement element)
        {
            InvalidCheck();
            return base.Contains(element);
        }
        public override IEnumerator<TElement> GetEnumerator()
        {
            InvalidCheck();
            return base.GetEnumerator();
        }

        private void InvalidCheck()
        {
            if(HasBeenMucked)
            {
                throw new ObjectDisposedException("Hand");
            }
        }

        void IDeckStackInternal<TElement>.Add(TElement element)
        {
            Contents.Add(element);
        }

        void IDeckStackInternal<TElement>.CheckEnabled()
        {
            // No-op
        }
    }
}

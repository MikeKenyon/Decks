using Decks.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Decks
{
    /// <summary>
    /// A base class for different types of deck stacks -- hands, table, tableau.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    internal class DeckStack<TElement> : IDeckStack<TElement> where TElement : class
    {
        public DeckStack(Deck<TElement> deck) {
            Deck = deck;
        }
        protected Deck<TElement> Deck { get; }
        protected internal List<TElement> Contents { get; } = new List<TElement>();

        public int Count { get { return Contents.Count; } }

        /// <summary>
        /// Checks if this hand contains that element.
        /// </summary>
        /// <param name="element"></param>
        /// <returns></returns>
        public virtual bool Contains(TElement element)
        {
            return Contents.Contains(element);
        }
        public virtual IEnumerator<TElement> GetEnumerator()
        {
            return Contents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        protected void MyElementCheck(TElement element)
        {
            if (!Contents.Contains(element))
            {
                throw new InvalidOperationException("Cannot add element from the tableau that's not in the tableau.");
            }
        }
        protected void HandCheck(IHand<TElement> hand, bool checkMucked = true)
        {
            if(!Deck.Hands.Contains(hand))
            {
                throw new InvalidOperationException("The given hand isn't from this deck.");
            }
            if(checkMucked && hand.HasBeenMucked)
            {
                throw new InvalidOperationException("Cannot use this hand, it's been mucked.");
            }
        }
        protected void CheckOperation(ValidOperations operation)
        {
            Deck.Check(operation);
        }
    }
}

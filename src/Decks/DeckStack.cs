﻿using Decks.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Decks.Internal;

namespace Decks
{
    /// <summary>
    /// A base class for different types of deck stacks -- hands, table, tableau.
    /// </summary>
    /// <typeparam name="TElement"></typeparam
    internal class DeckStack<TElement> : IDeckStackInternal<TElement> where TElement : class
    {
        public DeckStack(Internal.IDeckInternal<TElement> deck)
        {
            Deck = deck;
        }
        protected Internal.IDeckInternal<TElement> Deck { get; }
        protected List<TElement> Contents { get; } = new List<TElement>();

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

        /// <summary>
        /// Adds an eleemnt from one area to another.
        /// </summary>
        /// <param name="element">The element to add.</param>
        void IDeckStackInternal<TElement>.Add(TElement element)
        {
            Contents.Add(element);
        }

        /// <summary>
        /// Confirms that this stack is enabled, errors if it isn't.
        /// </summary>
        /// <exception cref="InvalidOperationException">The stack is not enabled.</exception>
        void IDeckStackInternal<TElement>.CheckEnabled()
        {
            //No op
        }

        public virtual IEnumerator<TElement> GetEnumerator()
        {
            return Contents.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }
        void IDeckVisitable<TElement>.Accept(IDeckVisitor<TElement> visitor)
        {
        }

        #region Helpers for Deck Stack Types
        /// <summary>
        /// Confirms that the element you're about to perform this on is in face your element.  
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <param name="message">The custome message</param>
        protected void CheckIsMyElement(TElement element, string message)
        {
            if (!Contents.Contains(element))
            {
                throw new InvalidElementException(message);
            }
        }
        /// <summary>
        /// Confirm that the operation in question is bin
        /// </summary>
        /// <param name="hand"></param>
        /// <param name="checkMucked"></param>
        protected void CheckOwnHand(IHand<TElement> hand, bool checkMucked = true)
        {
            if (!Deck.Hands.Contains(hand))
            {
                throw new InvalidOperationException("The given hand isn't from this deck.");
            }
            if (checkMucked && hand.HasBeenMucked)
            {
                throw new InvalidOperationException("Cannot use this hand, it's been mucked.");
            }
        }
        /// <summary>
        /// Checks to see if an operation is allowed or not.
        /// </summary>
        /// <param name="condition">Condition to check.</param>
        /// <param name="message">The error message if it's not allowed.</param>
        protected void CheckOperation(bool condition, string message)
        {
            if (!condition)
            {
                throw new InvalidOperationException(message);
            }
        }
        #endregion
    }
}

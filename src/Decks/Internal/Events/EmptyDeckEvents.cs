using Decks.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal.Events
{
    /// <summary>
    /// Default/empty implementation of the deck eventing interface.
    /// </summary>
    /// <typeparam name="TElement">The elements in the deck.</typeparam>
    internal class EmptyDeckEvents<TElement> : IDeckEvents<TElement> where TElement : class
    {
        private static Lazy<EmptyDeckEvents<TElement>> _singleton = new Lazy<EmptyDeckEvents<TElement>>();

        /// <summary>
        /// Gets the one and only version of this.
        /// </summary>
        internal static EmptyDeckEvents<TElement> Singleton { get { return _singleton.Value; } }

        void IDeckEvents<TElement>.Added(TElement element, DeckSide side, Location location)
        {
        }

        void IDeckEvents<TElement>.Adding(ref TElement element, ref DeckSide side, ref Location location)
        {
        }

        void IDeckEvents<TElement>.Dealing(ref int numberOfHands, ref uint handSize)
        {
        }

        void IDeckEvents<TElement>.Dealt(int numberOfHands, uint handSize)
        {
        }

        void IDeckEvents<TElement>.PlayedToTable<TElement1>(TElement1 card)
        {
        }
    }
}

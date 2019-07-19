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

        void IDeckEvents<TElement>.Discarded(ITable<TElement> table, TElement element)
        {

        }

        void IDeckEvents<TElement>.Discarding(ITable<TElement> table, TElement element)
        {

        }

        void IDeckEvents<TElement>.Drawing()
        {

        }

        void IDeckEvents<TElement>.DrawingInto(IHand<TElement> hand)
        {

        }

        void IDeckEvents<TElement>.DrawingInto(ITableau<TElement> tableau)
        {

        }

        void IDeckEvents<TElement>.DrawingInto(ITableau<TElement> tableau, IHand<TElement> hand, TElement element)
        {

        }

        void IDeckEvents<TElement>.Drew(TElement card)
        {

        }

        void IDeckEvents<TElement>.DrewInto(IHand<TElement> hand, TElement element)
        {

        }

        void IDeckEvents<TElement>.DrewInto(ITableau<TElement> tableau, TElement element)
        {

        }

        void IDeckEvents<TElement>.DrewInto(ITableau<TElement> tableau, IHand<TElement> hand, TElement element)
        {

        }

        void IDeckEvents<TElement>.Mucked(ITable<TElement> table)
        {

        }

        void IDeckEvents<TElement>.Mucked(IHand<TElement> hand)
        {

        }

        void IDeckEvents<TElement>.Mucking(ITable<TElement> table)
        {

        }

        void IDeckEvents<TElement>.Mucking(IHand<TElement> hand)
        {

        }

        int IDeckEvents<TElement>.PickElementToDiscard(ITableau<TElement> tableau)
        {
            return Extensions.Rand.Next(0, tableau.Count);
        }

        void IDeckEvents<TElement>.Played(TElement card)
        {

        }

        void IDeckEvents<TElement>.Played(IHand<TElement> hand, TElement element)
        {

        }

        void IDeckEvents<TElement>.Played(ITableau<TElement> tableau, TElement element)
        {

        }

        void IDeckEvents<TElement>.Playing(IHand<TElement> hand, TElement element)
        {

        }

        void IDeckEvents<TElement>.Playing(ITableau<TElement> tableau, TElement element)
        {

        }

        void IDeckEvents<TElement>.PutDiscardsBackIntoDrawPile()
        {

        }

        void IDeckEvents<TElement>.PuttingDiscardsBackIntoDrawPile()
        {

        }

        void IDeckEvents<TElement>.Shuffled(uint shuffleCount)
        {

        }

        void IDeckEvents<TElement>.Shuffling(uint v)
        {

        }
    }
}

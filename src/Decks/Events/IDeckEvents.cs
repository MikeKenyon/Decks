using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Events
{
    /// <summary>
    /// Events and interactions about a <see cref="IDeck{TElement}"/>.
    /// </summary>
    public interface IDeckEvents
    {
    }
    /// <summary>
    /// Element specific events.
    /// </summary>
    /// <typeparam name="TElement"></typeparam>
    public interface IDeckEvents<TElement> : IDeckEvents where TElement : class
    {
        void Adding(ref TElement element, ref DeckSide side, ref Location location);
        void Added(TElement element, DeckSide side, Location location);

        void Dealing(ref int numberOfHands, ref uint handSize);
        void Dealt(int numberOfHands, uint handSize);

        void Discarding(ITable<TElement> table, TElement element);
        void Discarded(ITable<TElement> table, TElement element);

        void Drawing();
        void Drew(TElement card);

        void DrawingInto(IHand<TElement> hand);
        void DrawingInto(ITableau<TElement> tableau);
        void DrawingInto(ITableau<TElement> tableau, IHand<TElement> hand, TElement element);
        void DrewInto(ITableau<TElement> tableau, TElement element);
        void DrewInto(IHand<TElement> hand, TElement element);
        void DrewInto(ITableau<TElement> tableau, IHand<TElement> hand, TElement element);

        void Mucking(ITable<TElement> table);
        void Mucking(IHand<TElement> hand);
        void Mucked(IHand<TElement> hand);
        void Mucked(ITable<TElement> table);

        int PickElementToDiscard(ITableau<TElement> tableau);

        void Playing(IHand<TElement> hand, TElement element);
        void Playing(ITableau<TElement> tableau, TElement element);
        void Played(TElement card);
        void Played(IHand<TElement> hand, TElement element);
        void Played(ITableau<TElement> tableau, TElement element);

        void PuttingDiscardsBackIntoDrawPile();
        void PutDiscardsBackIntoDrawPile();

        void Shuffling(uint v);
        void Shuffled(uint shuffleCount);
    }
}

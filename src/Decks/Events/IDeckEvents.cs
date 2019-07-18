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
        void PlayedToTable<TElement>(TElement card) where TElement : class;
        void Dealt(int numberOfHands, uint handSize);
    }
}

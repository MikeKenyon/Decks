using Decks.Events;
using Newtonsoft.Json;
using System;

namespace Decks.Internal
{
    [JsonConverter(typeof(Internal.Serialization.DeckSerializer<>))]
    internal interface IDeckInternal<TElement> : IDeck<TElement>, IDeckVisitable<TElement> where TElement : class
    {
        /// <summary>
        /// The collection of events that are in use.
        /// </summary>
        IDeckEvents<TElement> Events { get; }
        /// <summary>
        /// The internal representation of the <see cref="IDrawPile{TElement}"/>.
        /// </summary>
        Internal.IDrawPileInternal<TElement> DrawPileStack { get; }
        /// <summary>
        /// The internal representation of the <see cref="IDiscardPile{TElement}"/>.
        /// </summary>
        Internal.IDiscardPileInternal<TElement> DiscardPileStack { get; }
        /// <summary>
        /// The internal representation of the <see cref="ITableau{TElement}"/>.
        /// </summary>
        Internal.ITableauInternal<TElement> TableauStack { get; }
        /// <summary>
        /// The internal representation of the <see cref="ITable{TElement}"/>.
        /// </summary>
        Internal.ITableInternal<TElement> TableStack { get; }
        /// <summary>
        /// The type of elements in this <see cref="IDeck{TElement}"/>.
        /// </summary>
        /// <remarks>This is very handy when dealing witih a derived (and no longer generic deck class.</remarks>
        Type ElementType { get; }

        /// <summary>
        /// Removes a hand from those that the deck is aware of.
        /// </summary>
        /// <param name="hand">The hand to remove.</param>
        void RemoveHand(IHand<TElement> hand);

        /// <summary>
        /// Called before the deck is serialized.
        /// </summary>
        void Dehydrating();
        /// <summary>
        /// Called after the deck has ben deserialized.
        /// </summary>
        void Rehydrated();
    }
}

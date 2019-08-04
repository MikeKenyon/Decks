using Decks.Events;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal
{
    [JsonConverter(typeof(Internal.Serialization.DeckSerializer<>))]
    internal interface IDeckInternal<TElement> : IDeck<TElement>, IDeckVisitable<TElement> where TElement : class
    {
        IDeckEvents<TElement> Events { get; }
        Internal.IDrawPileInternal<TElement> DrawPileStack { get; }
        Internal.IDiscardPileInternal<TElement> DiscardPileStack { get; }
        Internal.ITableauInternal<TElement> TableauStack { get; }
        Internal.ITableInternal<TElement> TableStack { get; }
        Type ElementType { get; }

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

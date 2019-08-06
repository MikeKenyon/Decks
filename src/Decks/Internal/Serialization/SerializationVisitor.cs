using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Decks.Internal.Serialization
{
    /// <summary>
    /// A visitor that implements serialization.
    /// </summary>
    /// <typeparam name="TElement">The elements of the deck to serialize</typeparam>
    internal class SerializationVisitor<TElement> : IDeckVisitor<TElement> where TElement : class
    {
        public SerializationVisitor(JObject content, JsonSerializer serializer)
        {
            Content = content;
            Serializer = serializer;
        }

        public JObject Content { get; }
        public JsonSerializer Serializer { get; }
        public IDeckInternal<TElement> Deck { get; set; }
        public JArray Hands { get; set; }

        public void Visit(IDeckInternal<TElement> deck)
        {
            Deck = deck;

            if (deck.GetType() != typeof(Deck<TElement>))
            {
                Content.Add(JsonProperties.TypeName, deck.GetType().FullName);
            }
            if (deck.Options.GetType() != typeof(Configuration.DeckOptions))
            {
                Content.Add(JsonProperties.OptionsType, deck.Options.GetType().FullName);
            }
            Content.Add(JsonProperties.ElementType, deck.ElementType.FullName);
            Content.Add(JsonProperties.TotalCount, deck.Count);
            Content.Add(JsonProperties.Options, JToken.FromObject(deck.Options, Serializer));
        }

        public void Visit(IDrawPileInternal<TElement> drawPile)
        {
            var obj = new JObject();
            Serialize(obj, drawPile);
            obj.Add(JsonProperties.ShuffleCount, drawPile.ShuffleCount);
            Content.Add(JsonProperties.DrawPile, obj);
        }

        public void Visit(IDiscardPileInternal<TElement> discards)
        {
            var obj = new JObject();
            Serialize(obj, discards);
            Content.Add(JsonProperties.Discards, obj);
        }

        public void Visit(ITableInternal<TElement> table)
        {
            if (Deck.Options.Table.Enabled)
            {
                var obj = new JObject();
                Serialize(obj, table);
                Content.Add(JsonProperties.Table, obj);
            }
        }

        public void Visit(ITableauInternal<TElement> tableau)
        {
            if (Deck.Options.Tableau.Enabled)
            {
                var obj = new JObject();
                Serialize(obj, tableau);
                Content.Add(JsonProperties.Tableau, obj);
            }
        }

        public void Visit(IReadOnlyCollection<IHand<TElement>> hands)
        {
            Hands = FindHands();
        }


        public void Visit(IHandInternal<TElement> element)
        {
            var obj = new JObject();
            Serialize(obj, element);
            Hands.Add(obj);
        }

        #region Helpers

        private void Serialize(JObject obj, IDeckStackInternal<TElement> stack)
        {
            var array = new JArray();
            foreach (var item in stack)
            {
                array.Add(JToken.FromObject(item, Serializer));
            }
            obj.Add(JsonProperties.Elements, array);
        }

        private JArray FindHands()
        {
            if (Content.ContainsKey(JsonProperties.Hands))
            {
                return (JArray)Content[JsonProperties.Hands];
            }
            else
            {
                var array = new JArray();
                Content.Add(JsonProperties.Hands, array);
                return array;
            }
        }

        #endregion
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Decks.Internal.Serialization
{
    internal class DeserializationVisitor<TElement> : IDeckVisitor<TElement> where TElement : class
    {
        internal DeserializationVisitor(Newtonsoft.Json.Linq.JObject content, JsonSerializer serializer)
        {
            Content = content;
            Serializer = serializer;
        }

        public JObject Content { get; }
        public JsonSerializer Serializer { get; }
        public IDeckInternal<TElement> Deck { get; set; }

        public void Visit(IDeckInternal<TElement> deck)
        {
            Deck = deck;
        }

        public void Visit(IDrawPileInternal<TElement> drawPile)
        {
            var pile = (JObject)Content[JsonProperties.DrawPile];
            Load(pile, drawPile);
            drawPile.ShuffleCount = (uint) pile[JsonProperties.ShuffleCount];
        }

        public void Visit(IDiscardPileInternal<TElement> discards)
        {
            var pile = (JObject)Content[JsonProperties.Discards];
            Load(pile, discards);
        }

        public void Visit(ITableInternal<TElement> table)
        {
            if (Content.ContainsKey(JsonProperties.Table))
            {
                var pile = (JObject)Content[JsonProperties.Table];
                Load(pile, table);
            }
        }

        public void Visit(ITableauInternal<TElement> tableau)
        {
            if (Content.ContainsKey(JsonProperties.Tableau))
            {
                var pile = (JObject)Content[JsonProperties.Tableau];
                Load(pile, tableau);
            }
        }
        public void Visit(IReadOnlyCollection<IHand<TElement>> hands)
        {
            if(Content.ContainsKey(JsonProperties.Hands))
            {
                var handArray = (JArray)Content[JsonProperties.Hands];
                foreach(JObject handContent in handArray)
                {
                    var hand = Deck.Deal(1, 0).First();
                    Load(handContent, (IHandInternal<TElement>) hand);
                }
            }
        }


        public void Visit(IHandInternal<TElement> element)
        {
            // No-op
        }
        private void Load(JObject pile, IDeckStackInternal<TElement> stack)
        {
            var array = (JArray)pile[JsonProperties.Elements];

            foreach(var item in array)
            {
                stack.Add(item.ToObject<TElement>(Serializer));
            }
        }

    }
}

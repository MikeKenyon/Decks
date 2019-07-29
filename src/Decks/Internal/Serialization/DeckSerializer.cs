using System;
using System.Collections.Generic;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using Decks.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Decks.Internal.Serialization
{
    internal class DeckSerializer : JsonConverter
    {
        #region Public Interface
        public override bool CanConvert(Type objectType)
        {
            return typeof(IDeckInternal<>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var job = JObject.ReadFrom(reader) as JObject;

            var elementType = Type.GetType(job.Property(JsonProperties.ElementType).Value.ToString());


            var options = GetOptions(job.Property(JsonProperties.Options).Value.ToString(), serializer);
            var deck = Activator.CreateInstance(objectType, options, false);
            return deck;
        }
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dynamic deck = value;

            var root = new JObject();
            var deckType = FindDeckType(deck.GetType());
            root.Add(JsonProperties.ElementType, deckType.GetGenericArguments()[0]?.FullName ?? JsonProperties.NotAvailable);
            root.Add(JsonProperties.Options, JToken.FromObject(deck.Options, serializer));
            root.Add(JsonProperties.TotalCount, deck.TotalCount);

            root.Add(JsonProperties.DrawPile, JToken.FromObject(deck.DrawPile, serializer));

            root.WriteTo(writer);
        }

        #endregion


        #region Helpers

        #region Read

        #endregion

        #region Write

        #endregion

        #region Generic
        private object GetOptions(string text, JsonSerializer serializer)
        {
            var settings = new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Auto
            };
            return JsonConvert.DeserializeObject<DeckOptions>(text, settings);
        }


        private Type FindDeckType(Type type)
        {
            while(type != typeof(Object) && (!type.IsConstructedGenericType || type.GetGenericTypeDefinition() != typeof(Deck<>)))
            {
                type = type.BaseType;
            }
            return type;
        }
        #endregion
        #endregion
    }
}

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
    internal class DeckSerializer<TElement> : JsonConverter<IDeckInternal<TElement>> where TElement : class
    {
        public override IDeckInternal<TElement> ReadJson(JsonReader reader, Type objectType,
            IDeckInternal<TElement> existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);
            
            switch(token.Type)
            {
                case JTokenType.Null:
                    return null;
                case JTokenType.Object:
                    var job = (JObject)token;
                    var deck = MakeDeck(job);
                    var deser = new DeserializationVisitor<TElement>(job, serializer);
                    deck.Accept(deser);
                    deck.Rehydrated();
                    return deck;
                default:
                    throw new FormatException("A JObject representing the deck was expected");
            }
        }

        private IDeckInternal<TElement> MakeDeck(JObject job)
        {
            var type = Type.GetType(job[JsonProperties.TypeName].ToString());
            var elementType = Type.GetType(job[JsonProperties.ElementType].ToString());
            var optionType = typeof(DeckOptions);
            if (job.ContainsKey(JsonProperties.OptionsType))
            {
                optionType = Type.GetType(job[JsonProperties.OptionsType].ToString());
            }
            var options = job[JsonProperties.Options].ToObject(optionType);


            if(type.IsGenericType && !type.IsConstructedGenericType)
            {
                var cType = type.MakeGenericType(elementType);
                return (IDeckInternal<TElement>)Activator.CreateInstance(cType, options, false);
            }
            else
            {
                return (IDeckInternal<TElement>) Activator.CreateInstance(type, options, false);
            }
        }

        public override void WriteJson(JsonWriter writer, IDeckInternal<TElement> value, JsonSerializer serializer)
        {
            value.Dehydrating();

            var jobject = new JObject();
            var ser = new SerializationVisitor<TElement>(jobject, serializer);
            value.Accept(ser);
            jobject.WriteTo(writer);
        }
    }
}

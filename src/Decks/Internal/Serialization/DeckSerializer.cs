using Decks.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Reflection;

namespace Decks.Internal.Serialization
{
    internal class DeckSerializer : JsonConverter<IDeckInternal>
    {
        private Type GetElementType(JObject job)
        {
            var elementType = Type.GetType(job[JsonProperties.ElementType].ToString());
            return elementType;
        }

        private IDeckInternal MakeDeck(JObject job, Type elementType)
        {
            var type = typeof(Deck<>);
            if (job.ContainsKey(JsonProperties.TypeName))
            {
                type = Type.GetType(job[JsonProperties.TypeName].ToString());
            }
            var optionType = typeof(DeckOptions);
            if (job.ContainsKey(JsonProperties.OptionsType))
            {
                optionType = Type.GetType(job[JsonProperties.OptionsType].ToString());
            }
            var options = job[JsonProperties.Options].ToObject(optionType);


            if (type.IsGenericType && !type.IsConstructedGenericType)
            {
                var cType = type.MakeGenericType(elementType);
                return (IDeckInternal)Activator.CreateInstance(cType, options, false);
            }
            else
            {
                return (IDeckInternal)Activator.CreateInstance(type, options, false);
            }
        }


        public override void WriteJson(JsonWriter writer, IDeckInternal value, JsonSerializer serializer)
        {
            value.Dehydrating();

            var jobject = new JObject();
            var serType = typeof(SerializationVisitor<>).MakeGenericType(value.ElementType);
            dynamic ser = Activator.CreateInstance(serType, jobject, serializer);

            typeof(IDeckVisitable<>).MakeGenericType(value.ElementType).GetMethod("Accept").Invoke(value, new object[] { ser });

            jobject.WriteTo(writer);
        }

        public override IDeckInternal ReadJson(JsonReader reader, Type objectType, IDeckInternal existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var token = JToken.ReadFrom(reader);

            switch (token.Type)
            {
                case JTokenType.Null:
                    return null;
                case JTokenType.Object:
                    var job = (JObject)token;
                    var elementType = GetElementType(job);
                    var deck = MakeDeck(job, elementType);
                    var deserType = typeof(DeserializationVisitor<>).MakeGenericType(elementType);
                    var deser = Activator.CreateInstance(deserType, job, serializer);
                    typeof(IDeckVisitable<>).MakeGenericType(elementType).GetMethod("Accept").Invoke(deck, new object[] { deser });
                    deck.Rehydrated();
                    return deck;
                default:
                    throw new FormatException("A JObject representing the deck was expected");
            }
        }
    }
}

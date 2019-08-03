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
            return null;
        }

        public override void WriteJson(JsonWriter writer, IDeckInternal<TElement> value, JsonSerializer serializer)
        {
            var jobject = new JObject();
            var ser = new SerializationVisitor<TElement>(jobject, serializer);
            value.Accept(ser);
            jobject.WriteTo(writer);
        }
    }
}

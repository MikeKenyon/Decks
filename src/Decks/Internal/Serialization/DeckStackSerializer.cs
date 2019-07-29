using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Text;

namespace Decks.Internal.Serialization
{
    internal class DeckStackSerializer : JsonConverter
    {
        public override bool CanConvert(Type objectType)
        {
            return typeof(IDeckStackInternal<>).IsAssignableFrom(objectType);
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var array = JArray.ReadFrom(reader);

            return null;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            dynamic stack = value;
            var array = new JArray();
            foreach(var item in stack)
            {
                array.Add(JObject.FromObject(item, serializer));
            }
            array.WriteTo(writer);
        }
    }
}

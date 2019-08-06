using Decks.Common;
using Newtonsoft.Json;
using System;

namespace Decks.Internal.Serialization
{
    internal class PlayingCardSerializer : JsonConverter<Common.PlayingCard>
    {
        public override PlayingCard ReadJson(JsonReader reader, Type objectType, PlayingCard existingValue, bool hasExistingValue, JsonSerializer serializer)
        {
            var value = reader.Value.ToString();
            return PlayingCard.Parse(value);
        }

        public override void WriteJson(JsonWriter writer, PlayingCard value, JsonSerializer serializer)
        {
            writer.WriteValue(value.ToString());
        }
    }
}

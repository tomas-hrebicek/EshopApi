using System.Globalization;
using System.Text.Json.Serialization;
using System.Text.Json;

namespace Sample.Api
{
    internal sealed class IsoDateTimeConverter : JsonConverter<DateTime>
    {
        private const string ISODateTimeFormat = "yyyy'-'MM'-'dd'T'HH':'mm':'ss.FFFFFFFK";

        public override DateTime Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (DateTime.TryParseExact(reader.GetString(), ISODateTimeFormat, CultureInfo.InvariantCulture, DateTimeStyles.AdjustToUniversal, out DateTime dateTime))
            {
                return dateTime;
            }
            else
            {
                throw new JsonException("Invalid date format");
            }
        }

        public override void Write(Utf8JsonWriter writer, DateTime value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString(ISODateTimeFormat));
        }
    }
}
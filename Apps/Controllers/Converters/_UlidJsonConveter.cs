using System.Text.Json;
using System.Text.Json.Serialization;
using Apps.Utilities._Ulid;

namespace Apps.Controllers.Converters
{
    public class _UlidJsonConveter : JsonConverter<Ulid>
    {
        public override Ulid Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var ulidString = reader.GetString();

            if (ulidString == null) {
                return Ulid.Empty;
            }
            // Parse string dengan tanda hubung menjadi Ulid
            return _Ulid.FromFormattedString(ulidString);
        }

        public override void Write(Utf8JsonWriter writer, Ulid value, JsonSerializerOptions options)
        {
            // Tulis Ulid dengan tanda hubung ke dalam JSON
            writer.WriteStringValue(value.ToFormattedString());
        }
    }
}
using System.Text.Json;
using System.Text.Json.Serialization;
using SolucionApi.Enums;

namespace SolucionApi.Converters;

public class DaysEnumConverter : JsonConverter<DaysEnum>
{
    public override DaysEnum Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var days = new List<string>();
        while (reader.Read() && reader.TokenType != JsonTokenType.EndArray)
        {
            days.Add(reader.GetString());
        }

        DaysEnum result = DaysEnum.None;
        foreach (var day in days)
        {
            if (Enum.TryParse<DaysEnum>(day, true, out var dayOfWeek))
            {
                result |= dayOfWeek;
            }
        }

        return result;
    }

    public override void Write(Utf8JsonWriter writer, DaysEnum value, JsonSerializerOptions options)
    {
        writer.WriteStartArray();
        foreach (DaysEnum day in Enum.GetValues(typeof(DaysEnum)))
        {
            if (day != DaysEnum.None && value.HasFlag(day))
            {
                writer.WriteStringValue(day.ToString());
            }
        }
        writer.WriteEndArray();
    }
}

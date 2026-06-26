using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

public class DateFormatConverter : JsonConverter<DateTime>
{
    private const string Format = "dd/MM/yyyy";

    public override DateTime Read(
        ref Utf8JsonReader reader,
        Type typeToConvert,
        JsonSerializerOptions options)
    {
        string? dateStr = reader.GetString();

        if (DateTime.TryParseExact(
            dateStr,
            "dd/MM/yyyy",
            CultureInfo.InvariantCulture,
            DateTimeStyles.None,
            out DateTime date))
        {
            // força UTC para evitar erro no PostgreSQL
            return DateTime.SpecifyKind(date, DateTimeKind.Utc);
        }

        throw new JsonException("Formato de data inválido. Use dd/MM/yyyy.");
    }

    public override void Write(
        Utf8JsonWriter writer,
        DateTime value,
        JsonSerializerOptions options)
    {
        writer.WriteStringValue(
            value.ToString("dd/MM/yyyy", CultureInfo.InvariantCulture)
        );
    }
}
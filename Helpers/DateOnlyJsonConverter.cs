using System;
using System.Globalization;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace api_financeiro.Helpers
{
    public class DateOnlyJsonConverter : JsonConverter<DateOnly>
    {
        private static readonly string[] AcceptedFormats = new[] { "yyyy-MM-dd", "dd/MM/yyyy", "yyyyMMdd" };

        public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            if (reader.TokenType == JsonTokenType.String)
            {
                var s = reader.GetString();
                if (string.IsNullOrWhiteSpace(s))
                    throw new JsonException("Valor DateOnly vazio ou nulo.");

                // Tenta formatos padrão/culturais
                if (DateOnly.TryParse(s, CultureInfo.InvariantCulture, DateTimeStyles.None, out var result))
                    return result;

                // Tenta formatos explícitos
                if (DateOnly.TryParseExact(s, AcceptedFormats, CultureInfo.InvariantCulture, DateTimeStyles.None, out result))
                    return result;

                throw new JsonException($"Valor '{s}' não pôde ser convertido para DateOnly. Formato esperado: yyyy-MM-dd (exemplo).");
            }

            if (reader.TokenType == JsonTokenType.Number)
            {
                // Evite assumir significado de número; preferível falhar com mensagem clara
                if (reader.TryGetInt64(out var n))
                    throw new JsonException($"Valor numérico '{n}' não pode ser interpretado como DateOnly. Envie uma string ISO (yyyy-MM-dd).");
            }

            throw new JsonException($"Token inesperado ao desserializar DateOnly: {reader.TokenType}.");
        }

        public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.ToString("yyyy-MM-dd", CultureInfo.InvariantCulture));
        }
    }
}
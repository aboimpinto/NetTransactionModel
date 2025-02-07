using System.Text.Json;
using System.Text.Json.Serialization;
using NewTransactionModel.SpecificPayloads;

namespace NewTransactionModel.Model.Transaction.Converters;

public class AbstractTransactionConverter : JsonConverter<AbstractTransaction>
{
    public override AbstractTransaction Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        using var jsonDocument = JsonDocument.ParseValue(ref reader);

        var payloadKindElement = jsonDocument.RootElement;
        var payloadKind = payloadKindElement.GetProperty("PayloadKind").GetString();

        if (payloadKind == "e054b791-5e99-41aa-b870-a7201bc85ec3")
        {
            return JsonSerializer.Deserialize<ValidatedTransaction<RewardPayload>>(jsonDocument.RootElement.GetRawText());
        }

        throw new InvalidOperationException();
    }

    public override void Write(Utf8JsonWriter writer, AbstractTransaction value, JsonSerializerOptions options)
    {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
    }
}

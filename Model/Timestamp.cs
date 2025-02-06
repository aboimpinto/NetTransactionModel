using System.Text.Json.Serialization;
using NewTransactionModel.Model.Converters;

namespace NewTransactionModel.Model;

[JsonConverter(typeof(TimestampConverter))]
public record Timestamp(DateTime Value)
{
    public static Timestamp Empty { get; } = new(DateTime.MinValue);
    public static Timestamp Current { get; } = new(DateTime.UtcNow);

    public override string ToString() => this.Value.ToString("o");
}

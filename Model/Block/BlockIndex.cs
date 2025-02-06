using System.Text.Json.Serialization;
using NewTransactionModel.Model.Block.Converters;

namespace NewTransactionModel.Model.Block;

[JsonConverter(typeof(BlockIndexConverter))]
public record BlockIndex(long Value)
{
    public static BlockIndex Empty { get; } = new(-1);

    public override string ToString() => this.Value.ToString();
}

using System.Text.Json.Serialization;
using HushNetwork.Shared.Model.Block.Converters;

namespace HushNetwork.Shared.Model.Block;

[JsonConverter(typeof(BlockIndexConverter))]
public record BlockIndex(long Value)
{
    public static BlockIndex Empty { get; } = new(-1);

    public override string ToString() => Value.ToString();
}

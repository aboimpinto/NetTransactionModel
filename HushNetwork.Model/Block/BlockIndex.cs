using System.Text.Json.Serialization;
using HushNetwork.Model.Block.Converters;

namespace HushNetwork.Model.Block;

[JsonConverter(typeof(BlockIndexConverter))]
public record BlockIndex(long Value)
{
    public static BlockIndex Empty { get; } = new(-1);

    public override string ToString() => this.Value.ToString();
}

using System.Text.Json.Serialization;
using HushNetwork.Shared.Model.Block.Converters;

namespace HushNetwork.Shared.Model.Block;

[JsonConverter(typeof(BlockIdConverter))]
public readonly record struct BlockId(Guid Value)
{
    public static BlockId Empty { get; } = new(Guid.Empty);
    public static BlockId NewBlockId { get; } = new(Guid.NewGuid());
    public static BlockId GenesisBlockId { get; } = new(Guid.Parse("7359ccef-763b-4adf-8f33-45db84c5121c"));

    public override string ToString() => Value.ToString();
}

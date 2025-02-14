using System.Text.Json;
using HushNetwork.Shared.Model.Transaction;

namespace HushNetwork.Shared.Model.Block.Unsigned;

public record UnsignedBlock(
    BlockId BlockId,
    Timestamp CreationTimeStamp,
    BlockIndex BlockIndex,
    BlockId PreviousBlockId,
    BlockId NextBlockId,
    AbstractTransaction[] Transactions)
{
    public string ToJson() => 
        JsonSerializer.Serialize(this);

    public string CreateSignature(string privateKey) => 
        DigitalSignature.SignMessage(ToJson(), privateKey);
}

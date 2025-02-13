using System.Text.Json;
using NewTransactionModel.Model.Transaction;

namespace NewTransactionModel.Model.Block.Unsigned;

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
        SigningKeys.SignMessage(this.ToJson(), privateKey);
}

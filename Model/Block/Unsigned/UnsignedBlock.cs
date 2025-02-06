using NewTransactionModel.Model.Transaction;
using NewTransactionModel.Model.Transaction.Unsigned;

namespace NewTransactionModel.Model.Block.Unsigned;

public record UnsignedBlock(
    BlockId BlockId,
    Timestamp CreationTimeStamp,
    BlockIndex BlockIndex,
    BlockId PreviousBlockId,
    BlockId NextBlockId,
    AbstractTransaction[] Transactions)
{
    
}

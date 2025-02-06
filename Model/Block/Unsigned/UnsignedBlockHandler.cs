using NewTransactionModel.Model.Transaction;
using NewTransactionModel.Model.Transaction.Unsigned;

namespace NewTransactionModel.Model.Block.Unsigned;

public static class UnsignedBlockHandler
{
    public static UnsignedBlock CreateGenesis(
        Timestamp creationTimeStamp,
        BlockId nextBlockId) => 
        CreateNew(
                BlockId.GenesisBlockId,
                new BlockIndex(1),
                creationTimeStamp,
                BlockId.Empty,
                nextBlockId);

    public static UnsignedBlock CreateNew(
        BlockId blockId,
        BlockIndex blockIndex, 
        Timestamp creationTimeStamp,
        BlockId previousBlockId, 
        BlockId nextBlockId, 
        AbstractTransaction[] transactions)
    {
        return new UnsignedBlock(
            blockId,
            creationTimeStamp,
            blockIndex,
            previousBlockId,
            nextBlockId,
            transactions);
    }

    public static UnsignedBlock CreateNew(
        BlockId blockId,
        BlockIndex blockIndex, 
        Timestamp creationTimeStamp,
        BlockId previousBlockId, 
        BlockId nextBlockId)
    {
        return new UnsignedBlock(
            blockId,
            creationTimeStamp,
            blockIndex,
            previousBlockId,
            nextBlockId,
            []);
    }
}

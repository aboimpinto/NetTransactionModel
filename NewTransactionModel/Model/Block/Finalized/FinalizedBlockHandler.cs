using NewTransactionModel.Model.Block.Signed;

namespace NewTransactionModel.Model.Block.Finalized;

public static class FinalizedBlockHandler
{
    public static (SignedBlock, string) ExtractSignedBlock(this FinalizedBlock finalizedBlock) =>
        (new(
            finalizedBlock.BlockId,
            finalizedBlock.CreationTimeStamp,
            finalizedBlock.BlockIndex,
            finalizedBlock.PreviousBlockId,
            finalizedBlock.NextBlockId,
            finalizedBlock.BlockProducerSignature,
            finalizedBlock.Transactions), 
        finalizedBlock.Hash);
}

using NewTransactionModel.Model.Block.Finalized;

namespace NewTransactionModel.Model.Block.Signed;

public static class SignedBlockHandler
{
    public static FinalizedBlock FinalizeIt(this SignedBlock signedBlock)
    {
        return new FinalizedBlock(
            signedBlock, 
            signedBlock.GetHashCode().ToString());
    }
}

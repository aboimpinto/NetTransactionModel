using System.Text.Json;
using NewTransactionModel.Model.Block.Finalized;
using NewTransactionModel.Model.Block.Unsigned;

namespace NewTransactionModel.Model.Block.Signed;

public static class SignedBlockHandler
{
    public static FinalizedBlock FinalizeIt(this SignedBlock signedBlock)
    {
        return new FinalizedBlock(
            signedBlock, 
            signedBlock.GetHashCode().ToString());
    }

    public static UnsignedBlock ExtractUnsignedBlock(this SignedBlock signedBlock) =>
        UnsignedBlockHandler.CreateNew(
            signedBlock.BlockId,
            signedBlock.BlockIndex,
            signedBlock.CreationTimeStamp,
            signedBlock.PreviousBlockId,
            signedBlock.NextBlockId,
            signedBlock.Transactions);

    public static bool CheckBlockSignature(this SignedBlock signedBlock)
    {
        var unsignedBlock = signedBlock.ExtractUnsignedBlock();

        return SigningKeys.VerifySignature(
            JsonSerializer.Serialize(unsignedBlock), 
            signedBlock.BlockProducerSignature.Signature, 
            signedBlock.BlockProducerSignature.Signatory);
    }

    public static bool IsBlockValid(this FinalizedBlock finalizedBlock) => 
        !finalizedBlock.CheckBlockHashAndSignature() ||
        !finalizedBlock.Transactions.All(transaction => transaction.CheckSignature());

    private static bool CheckBlockHashAndSignature(this FinalizedBlock finalizedBlock) =>
        finalizedBlock.IsBlockHashValid() &&
        finalizedBlock.IsBlockSignatureValid();

    public static bool IsBlockHashValid(this FinalizedBlock finalizedBlock)
    {
        var (signedBlock, hash) = finalizedBlock.ExtractSignedBlock();

        return signedBlock.CheckHash(hash);
    }

    public static bool IsBlockSignatureValid(this FinalizedBlock finalizedBlock) => 
        finalizedBlock.ExtractSignedBlock().Item1.CheckBlockSignature();

    public static bool CheckHash(this SignedBlock signedBlock, string hash) => 
        signedBlock.GetHashCode().ToString() == hash;
}
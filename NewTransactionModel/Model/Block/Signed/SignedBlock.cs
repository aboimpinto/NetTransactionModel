using NewTransactionModel.Model.Block.Unsigned;
using NewTransactionModel.Model.Transaction;

namespace NewTransactionModel.Model.Block.Signed;

public record SignedBlock : UnsignedBlock
{
    public SignatureInfo BlockProducerSignature { get; init; }

    public SignedBlock(
        UnsignedBlock unsignedBlock, 
        SignatureInfo blockProducerSignature)
        : base(unsignedBlock)
    {
        BlockProducerSignature = blockProducerSignature;
    }

    public SignedBlock(
        BlockId BlockId,
        Timestamp CreationTimeStamp,
        BlockIndex BlockIndex,
        BlockId PreviousBlockId,
        BlockId NextBlockId,
        SignatureInfo BlockProducerSignature,
        AbstractTransaction[] Transactions)
        : base(new UnsignedBlock(
            BlockId,
            CreationTimeStamp,
            BlockIndex,
            PreviousBlockId,
            NextBlockId,
            Transactions))
    {
        this.BlockProducerSignature = BlockProducerSignature;
    }

    public bool CheckSignature()
    {
        var unsignedBlock = this.ExtractUnsignedBlock();

        return SigningKeys.VerifySignature(
            unsignedBlock.ToJson(), 
            this.BlockProducerSignature.Signature, 
            this.BlockProducerSignature.Signatory);
    }
}

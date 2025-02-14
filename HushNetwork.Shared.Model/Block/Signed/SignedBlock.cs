using HushNetwork.Shared.Model.Block.Unsigned;
using HushNetwork.Shared.Model.Transaction;

namespace HushNetwork.Shared.Model.Block.Signed;

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

        return DigitalSignature.VerifySignature(
            unsignedBlock.ToJson(),
            BlockProducerSignature.Signature,
            BlockProducerSignature.Signatory);
    }
}

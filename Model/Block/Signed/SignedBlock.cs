using NewTransactionModel.Model.Block.Unsigned;
using NewTransactionModel.Model.Transaction;

namespace NewTransactionModel.Model.Block.Signed;

public record SignedBlock : UnsignedBlock
{
    public SignatureInfo BlockProducerSignature { get; init; }

    public SignedBlock(UnsignedBlock unsignedBlock, SignatureInfo blockProducerSignature)
        : base(unsignedBlock)
    {
        BlockProducerSignature = blockProducerSignature;
    }
}

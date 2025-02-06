using NewTransactionModel.Model.Block.Signed;

namespace NewTransactionModel.Model.Block.Finalized;

public record FinalizedBlock : SignedBlock
{
    public string Hash { get; init; }

    public FinalizedBlock(SignedBlock original, string hash) : base(original)
    {
        this.Hash = hash;
    }
}

using System.Text.Json.Serialization;
using HushNetwork.Model.Block.Signed;
using HushNetwork.Model.Block.Unsigned;
using HushNetwork.Model.Transaction;

namespace HushNetwork.Model.Block.Finalized;

public record FinalizedBlock : SignedBlock
{
    public string Hash { get; init; }

    public FinalizedBlock(
        SignedBlock signedBlock, 
        string hash) 
        : base(signedBlock)
    {
        this.Hash = hash;
    }

    [JsonConstructor]
    public FinalizedBlock(
        BlockId BlockId,
        Timestamp CreationTimeStamp,
        BlockIndex BlockIndex,
        BlockId PreviousBlockId,
        BlockId NextBlockId,
        SignatureInfo BlockProducerSignature,
        string Hash,
        AbstractTransaction[] Transactions) 
        : base(new SignedBlock(
            new UnsignedBlock(
                BlockId,
                CreationTimeStamp,
                BlockIndex,
                PreviousBlockId,
                NextBlockId,
                Transactions), 
            BlockProducerSignature))
    {
        this.Hash = Hash;   
    }
}

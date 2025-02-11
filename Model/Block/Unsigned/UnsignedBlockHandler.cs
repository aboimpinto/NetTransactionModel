using System.Text.Json;
using NewTransactionModel.Model.Block.Signed;
using NewTransactionModel.Model.Transaction;

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

    public static SignedBlock SignIt(this UnsignedBlock unsignedBlock, SignatureInfo blockProducerSignature) => 
        new(
            unsignedBlock, 
            blockProducerSignature);

    public static SignedBlock SignIt(this UnsignedBlock unsignedBlock, string publickey, string privateKey) => 
        new(
            unsignedBlock, 
            new SignatureInfo(publickey, SigningKeys.SignMessage(JsonSerializer.Serialize(unsignedBlock), privateKey)));
}

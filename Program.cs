using System.Text.Json;
using NewTransactionModel.Model;
using NewTransactionModel.Model.Block;
using NewTransactionModel.Model.Block.Finalized;
using NewTransactionModel.Model.Block.Signed;
using NewTransactionModel.Model.Block.Unsigned;
using NewTransactionModel.Model.Transaction;
using NewTransactionModel.Model.Transaction.Signed;
using NewTransactionModel.Model.Transaction.Unsigned;
using NewTransactionModel.SpecificPayloads;

// Block example usage
// Step 1: Create Genesis Block
var genesisBlock = UnsignedBlockHandler.CreateGenesis(
    Timestamp.Current,
    BlockId.NewBlockId);

// Transaction example usage
// Step 1: Client creates an unsigned transaction
var unsignedTransaction = UnsignedTransactionHandler.CreateNew(
    RewardPayloadHandler.RewardPayloadKind,
    Timestamp.Current,
    new RewardPayload("HUSH", "5"));

// Step 2: Client signs the transaction
var signedTransaction = unsignedTransaction.SignByUser(new SignatureInfo("Paulo Aboim Pinto", "Paulo's signature"));

// Step 3: Validator validates the transaction
var validatedTransaction = signedTransaction.SignByValidator(new SignatureInfo("Validator", "Validator's signature"));

var rewardTransactionJson = JsonSerializer.Serialize(validatedTransaction);
Console.WriteLine(rewardTransactionJson);

Console.WriteLine();
Console.WriteLine();

var genesisBlockWithTransactions = genesisBlock with
{
    Transactions = [..genesisBlock.Transactions, validatedTransaction]
};

var signedBlock = genesisBlockWithTransactions.SignIt(new SignatureInfo("BlockProducer", "BlockProducer's signature"));

var finalizedBlock = signedBlock.FinalizeIt();
var finalizedBlockJson = JsonSerializer.Serialize(finalizedBlock);

Console.WriteLine(finalizedBlockJson);

var deserializedBlock = JsonSerializer.Deserialize<FinalizedBlock>(finalizedBlockJson);
var deserializedBlockJson = JsonSerializer.Serialize(deserializedBlock);

Console.WriteLine();
Console.WriteLine();

Console.WriteLine(deserializedBlockJson);

if (finalizedBlock == deserializedBlock)
{
    Console.WriteLine("Hashes are equal");
}
    
Console.ReadLine();
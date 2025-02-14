using System.Collections.Concurrent;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using HushNetwork.SpecificPayloads;
using NewTransactionModel;
using NewTransactionModel.SpecificPayloads;
using HushNetwork.Shared.Model;
using HushNetwork.Shared.Model.Transaction;
using HushNetwork.Shared.Model.Transaction.Unsigned;
using HushNetwork.Shared.Model.Transaction.Signed;
using HushNetwork.Shared.Model.Block;
using HushNetwork.Shared.Model.Block.Unsigned;
using HushNetwork.Shared.Model.Block.Signed;

var dbContext = new HushNetworkDbContext();
dbContext.Database.Migrate();

// var nbrTransactionsPerBlock = 10000000;
// var nbrTransactionsPerBlock = 1000000;
var nbrTransactionsPerBlock = 1;

var userKeys = new HushNetwork.DigitalSignature();

var transactions = new ConcurrentBag<AbstractTransaction>();

DateTime startTimeGeneration = DateTime.Now;
Stopwatch stopwatchGeneration = Stopwatch.StartNew(); // More precise timing

Console.WriteLine($"Generation starting time before: {startTimeGeneration:HH:mm:ss.fff}"); // Include milliseconds

for(int i = 0; i < nbrTransactionsPerBlock; i++)
{
    var validEmptyTransaction = UnsignedTransactionHandler
        .CreateNew(
            EmptyPayloadHandler.EmptyPayloadKind,
            Timestamp.Current,
            new EmptyPayload())
        .SignByUser(userKeys.PublicAddress, userKeys.PrivateKey)
        .SignByValidator(userKeys.PublicAddress, userKeys.PrivateKey);

    transactions.Add(validEmptyTransaction);
}

DateTime endTimeGeneration = DateTime.Now;
stopwatchGeneration.Stop();
TimeSpan elapsedTimeGeneration = stopwatchGeneration.Elapsed;


Console.WriteLine($"Time after: {endTimeGeneration:HH:mm:ss.fff}");
Console.WriteLine($"Elapsed time for generate EmptyTransactions: {elapsedTimeGeneration.TotalMilliseconds} ms"); 
Console.WriteLine($"Elapsed time for generate EmptyTransactions (Stopwatch): {stopwatchGeneration.ElapsedMilliseconds} ms"); 


// Block example usage
// Step 1: Create Genesis Block
var genesisBlock = UnsignedBlockHandler.CreateGenesis(
    Timestamp.Current,
    BlockId.NewBlockId);

// Transaction example usage
// Step 1: Client creates an unsigned transaction
// Step 2: Client signs the transaction
// Step 3: Validator validates the transaction
var validatedRewardTransaction = RewardPayloadHandler
    .CreateRewardTransaction("HUSH", DecimalStringConverter.DecimalToString(5m))
    .SignByUser(userKeys.PublicAddress, userKeys.PrivateKey)
    .SignByValidator(userKeys.PublicAddress, userKeys.PrivateKey);


Console.WriteLine();
Console.WriteLine();

var genesisBlockWithTransactions = genesisBlock with
{
    Transactions = [..genesisBlock.Transactions, validatedRewardTransaction]
};

DateTime startTimeAdd = DateTime.Now;
Stopwatch stopwatchAdd = Stopwatch.StartNew(); // More precise timing

Console.WriteLine($"Add starting time before: {startTimeAdd.ToString("HH:mm:ss.fff")}"); // Include milliseconds

var transactionFromMemPool = transactions.TakeAndRemove(nbrTransactionsPerBlock);
var withAllTransactions = genesisBlockWithTransactions with
{
    Transactions = [..genesisBlockWithTransactions.Transactions, ..transactionFromMemPool]
};  

DateTime endTimeAdd = DateTime.Now;
stopwatchAdd.Stop();
TimeSpan elapsedTimeAdd = stopwatchAdd.Elapsed;


Console.WriteLine($"Time after: {endTimeAdd:HH:mm:ss.fff}");
Console.WriteLine($"Elapsed time for adding EmptyTransactions to the block: {elapsedTimeAdd.TotalMilliseconds} ms"); 
Console.WriteLine($"Elapsed time for adding EmptyTransactions to the block (Stopwatch): {stopwatchAdd.ElapsedMilliseconds} ms"); 


var finalizedBlock = withAllTransactions
    .SignIt(userKeys.PublicAddress, userKeys.PrivateKey)
    .FinalizeIt();

Console.WriteLine(finalizedBlock.ToJson());

// var deserializedBlock = JsonSerializer.Deserialize<FinalizedBlock>(finalizedBlockJson);
// var deserializedBlockJson = JsonSerializer.Serialize(deserializedBlock);

// Console.WriteLine();
// Console.WriteLine();

// Console.WriteLine(deserializedBlockJson);


Console.WriteLine();
Console.WriteLine();
Console.WriteLine("Checking block consistency...");

var blockChecked = finalizedBlock.IsBlockValid();
Console.WriteLine($"Block is valid: {blockChecked}");

// dbContext.Blocks.Add(finalizedBlock.ToBlockEntity());
// dbContext.SaveChanges();

Console.ReadLine();
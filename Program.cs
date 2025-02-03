using System.Text.Json;

Console.WriteLine("Hello, World!");

// Example usage
// Step 1: Client creates an unsigned transaction
var unsignedTransaction = UnsignedTransaction<RewardPayload>.Create(
    Guid.NewGuid(),
    DateTime.UtcNow.ToString(),
    new RewardPayload("HUSH", "5"));

// Step 2: Client signs the transaction
var signedTransaction = new SignedTransaction<RewardPayload>(unsignedTransaction, "Paulo", "Paulo's signature");

// Step 3: Validator validates the transaction
var validatedTransaction = new ValidatedTransaction<RewardPayload>(signedTransaction, "Validator", "Validator's signature");

// Step 4: Block Producer finalizes the transaction
var finalizedTransaction = new FinalizedTransaction<RewardPayload>(validatedTransaction, 1, "Producer", "Producer's signature", "xxxxxxxx");

var json = JsonSerializer.Serialize(finalizedTransaction);
Console.WriteLine(json);


public record TransactionId(string Value);

public record TransactionPayloadKind();

public record RewardPayload(string Token, string Amount): TransactionPayloadKind;

// Step 1: Client creates an UnsignedTransaction
public record UnsignedTransaction<T> where T: TransactionPayloadKind
{
    public TransactionId TransactionId { get; init; }
    public Guid PayloadKind { get; init; }
    public string CreationTimeStamp { get; init; }
    public T Payload { get; init; }

    internal UnsignedTransaction(
        TransactionId transactionId,
        Guid payloadKind,
        string creationTimeStamp,
        T payload)
    {
        TransactionId = transactionId;
        PayloadKind = payloadKind;
        CreationTimeStamp = creationTimeStamp;

        Payload = payload;
    }

    public static UnsignedTransaction<T> Create<T>(Guid payloadType, string CreationTimeStamp, T payload)
        where T : TransactionPayloadKind => 
        new(
            new TransactionId(Guid.NewGuid().ToString()),
            payloadType,
            CreationTimeStamp,
            payload);
}


// Step 2: Client signs the transaction
public record SignedTransaction<T>: UnsignedTransaction<T> where T: TransactionPayloadKind
{
    public string Signatory { get; init; }
    public string Signature { get; init; }

    public SignedTransaction(UnsignedTransaction<T> unsignedTransaction, string signatory, string signature)
      : base(
        unsignedTransaction.TransactionId, 
        unsignedTransaction.PayloadKind,
        unsignedTransaction.CreationTimeStamp, 
        unsignedTransaction.Payload)
    {
        Signatory = signatory;
        Signature = signature;
    }
}

// Step 3: Validator validates the transaction
public record ValidatedTransaction<T>: SignedTransaction<T> where T: TransactionPayloadKind
{
    public ValidatorInfo ValidatedBy { get; init; }

    public ValidatedTransaction(SignedTransaction<T> signedTransaction, string validatorSignatory, string validatorSignature)
      : base(signedTransaction, signedTransaction.Signatory, signedTransaction.Signature)
    {
        ValidatedBy = new ValidatorInfo(validatorSignatory, validatorSignature);
    }
}

// Step 4: Block Producer finalizes the transaction
public record FinalizedTransaction<T>: ValidatedTransaction<T> where T: TransactionPayloadKind
{
    public int BlockIndex { get; init; }
    public ProducerInfo ProducedBy { get; init; }
    public string Hash { get; init; }

    public FinalizedTransaction(ValidatedTransaction<T> validatedTransaction, int blockIndex, string producerSignatory, string producerSignature, string hash)
      : base(validatedTransaction, validatedTransaction.ValidatedBy.Signatory, validatedTransaction.ValidatedBy.Signature)
    {
        BlockIndex = blockIndex;
        ProducedBy = new ProducerInfo(producerSignatory, producerSignature);
        Hash = hash;
    }
}

public record ValidatorInfo(string Signatory, string Signature);

public record ProducerInfo(string Signatory, string Signature);



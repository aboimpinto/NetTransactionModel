using System.Text.Json;
using HushNetwork.Model.Transaction.Unsigned;

namespace HushNetwork.Model.Transaction.Signed;

// Step 2: Client signs the transaction
public record SignedTransaction<T>: UnsignedTransaction<T>
    where T: ITransactionPayloadKind
{
    public SignatureInfo UserSignature { get; init; }

    public SignedTransaction(
        UnsignedTransaction<T> unsignedTransaction, 
        SignatureInfo signature)
      : base(
            unsignedTransaction.TransactionId, 
            unsignedTransaction.PayloadKind,
            unsignedTransaction.TransactionTimeStamp, 
            unsignedTransaction.Payload,
            unsignedTransaction.PayloadSize)
    {
        this.UserSignature = signature;
    }

    public override string ToJson() => JsonSerializer.Serialize(this);
}



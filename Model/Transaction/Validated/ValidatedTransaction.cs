using System.Text.Json.Serialization;
using NewTransactionModel.Model;
using NewTransactionModel.Model.Transaction;
using NewTransactionModel.Model.Transaction.Signed;
using NewTransactionModel.Model.Transaction.Unsigned;

// Step 3: Validator validates the transaction
public record ValidatedTransaction<T>: SignedTransaction<T> 
    where T: TransactionPayloadKind
{
    public SignatureInfo ValidatorSignature { get; init; }

    public ValidatedTransaction(
        SignedTransaction<T> signedTransaction, 
        SignatureInfo validatorSignature) 
        : base(
            signedTransaction, 
            signedTransaction.UserSignature)
    {
        this.ValidatorSignature = validatorSignature;
    }

    [JsonConstructor]
    public ValidatedTransaction(
        TransactionId TransactionId,
        Guid PayloadKind,
        Timestamp TransactionTimeStamp,
        T Payload,
        SignatureInfo UserSignature,
        SignatureInfo ValidatorSignature)
        : base(
            new SignedTransaction<T>(
                new UnsignedTransaction<T>(
                    TransactionId,
                    PayloadKind,
                    TransactionTimeStamp,
                    Payload), 
                UserSignature),
            UserSignature)
    {
        this.ValidatorSignature = ValidatorSignature;
    }
}



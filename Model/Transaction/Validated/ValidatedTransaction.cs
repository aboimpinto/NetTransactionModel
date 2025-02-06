using NewTransactionModel.Model.Transaction;
using NewTransactionModel.Model.Transaction.Signed;

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
}



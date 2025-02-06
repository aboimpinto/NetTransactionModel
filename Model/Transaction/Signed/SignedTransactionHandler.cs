namespace NewTransactionModel.Model.Transaction.Signed;

public static class SignedTransactionHandler
{
    public static ValidatedTransaction<T> SignByValidator<T>(this SignedTransaction<T> signedTransaction, SignatureInfo validatorSignature)
        where T : TransactionPayloadKind =>
        new(
            signedTransaction,
            validatorSignature);
}

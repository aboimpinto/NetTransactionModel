using NewTransactionModel.Model.Transaction.Signed;
using NewTransactionModel.Model.Transaction.Unsigned;

namespace NewTransactionModel.Model.Transaction.Validated;

public static class ValidatorTransactionHandler
{
    public static SignedTransaction<T> ExtractSignedTransaction<T>(this ValidatedTransaction<T> validatedTransaction)
        where T: ITransactionPayloadKind => 
            new(
                new UnsignedTransaction<T>(
                    validatedTransaction.TransactionId, 
                    validatedTransaction.PayloadKind,
                    validatedTransaction.TransactionTimeStamp, 
                    validatedTransaction.Payload,
                    validatedTransaction.PayloadSize), 
                validatedTransaction.UserSignature);
}

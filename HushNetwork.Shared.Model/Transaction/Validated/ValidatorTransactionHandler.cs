using HushNetwork.Shared.Model.Transaction.Signed;
using HushNetwork.Shared.Model.Transaction.Unsigned;

namespace HushNetwork.Shared.Model.Transaction.Validated;

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

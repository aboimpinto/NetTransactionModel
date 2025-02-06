using NewTransactionModel.Model.Transaction.Signed;

namespace NewTransactionModel.Model.Transaction.Unsigned;

public static class UnsignedTransactionHandler
{
    public static UnsignedTransaction<T> CreateNew<T>(Guid payloadType, Timestamp CreationTimeStamp, T payload)
        where T : TransactionPayloadKind => 
        new(
            TransactionId.NewTransactionId,
            payloadType,
            CreationTimeStamp,
            payload);

    public static SignedTransaction<T> SignByUser<T>(this UnsignedTransaction<T> unsignedTransaction, SignatureInfo userSignature)
        where T : TransactionPayloadKind =>
        new(
            unsignedTransaction,
            userSignature);
}


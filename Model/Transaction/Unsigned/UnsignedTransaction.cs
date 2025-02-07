namespace NewTransactionModel.Model.Transaction.Unsigned;

public record UnsignedTransaction<T> : AbstractTransaction
    where T: TransactionPayloadKind
{
    public T Payload { get; init; }

    public UnsignedTransaction(
        TransactionId transactionId,
        Guid payloadKind,
        Timestamp TransactionTimeStamp,
        T payload) : 
        base(
            transactionId, 
            payloadKind, 
            TransactionTimeStamp)
    {
        Payload = payload;
    }
}

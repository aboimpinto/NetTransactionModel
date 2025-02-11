namespace NewTransactionModel.Model.Transaction.Unsigned;

public record UnsignedTransaction<T> : AbstractTransaction
    where T: ITransactionPayloadKind
{
    public T Payload { get; init; }

    public long PayloadSize { get; init; }

    public UnsignedTransaction(
        TransactionId transactionId,
        Guid payloadKind,
        Timestamp TransactionTimeStamp,
        T payload,
        long payloadSize) : 
        base(
            transactionId, 
            payloadKind, 
            TransactionTimeStamp)
    {
        this.Payload = payload;
        this.PayloadSize = payloadSize;
    }

    public override bool CheckSignature()
    {
        return true;
    }
}
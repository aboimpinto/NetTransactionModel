using System.Text.Json;

namespace HushNetwork.Shared.Model.Transaction.Unsigned;

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
        Payload = payload;
        PayloadSize = payloadSize;
    }

    public override bool CheckUserSignature()
    {
        return true;
    }

    public override bool CheckValidatorSignature()
    {
        return true;
    }

    public override string ToJson() => 
        JsonSerializer.Serialize(this);

    public override string CreateSignature(string privateKey) => 
        DigitalSignature.SignMessage(ToJson(), privateKey);
}
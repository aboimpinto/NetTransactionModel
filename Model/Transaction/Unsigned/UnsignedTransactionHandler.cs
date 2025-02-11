using System.Text.Json;
using NewTransactionModel.Model.Transaction.Signed;

namespace NewTransactionModel.Model.Transaction.Unsigned;

public static class UnsignedTransactionHandler
{
    public static UnsignedTransaction<T> CreateNew<T>(Guid payloadType, Timestamp CreationTimeStamp, T payload)
        where T : ITransactionPayloadKind => 
        new(
            TransactionId.NewTransactionId,
            payloadType,
            CreationTimeStamp,
            payload,
            GetObjectSize<T>(payload));

    public static SignedTransaction<T> SignByUser<T>(this UnsignedTransaction<T> unsignedTransaction, SignatureInfo userSignature)
        where T : ITransactionPayloadKind =>
        new(
            unsignedTransaction,
            userSignature);

    public static SignedTransaction<T> SignByUser<T>(this UnsignedTransaction<T> unsignedTransaction, string publickey, string privateKey)
        where T : ITransactionPayloadKind =>
        new(
            unsignedTransaction,
            new SignatureInfo(publickey, SigningKeys.SignMessage(JsonSerializer.Serialize(unsignedTransaction), privateKey)));

    private static long GetObjectSize<T>(T obj)
    {
        if (obj == null) return 0;

        try
        {
            var options = new JsonSerializerOptions();
            using (MemoryStream stream = new MemoryStream())
            {
                JsonSerializer.Serialize(stream, obj, options);
                return stream.Length;
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error serializing object: {ex.Message}");
            return 0; // Or throw the exception if you prefer
        }
    }
}


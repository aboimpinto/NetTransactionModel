using System.Text.Json.Serialization;
using HushNetwork.Shared.Model.Transaction.Converters;

namespace HushNetwork.Shared.Model.Transaction;

[JsonConverter(typeof(AbstractTransactionConverter))]
public abstract record AbstractTransaction(
    TransactionId TransactionId, 
    Guid PayloadKind, 
    Timestamp TransactionTimeStamp)
{

    public abstract bool CheckValidatorSignature();

    public abstract bool CheckUserSignature();

    public abstract string ToJson();

    public abstract string CreateSignature(string privateKey);

}
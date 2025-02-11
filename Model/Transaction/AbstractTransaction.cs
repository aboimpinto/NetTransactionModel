using System.Text.Json.Serialization;
using NewTransactionModel.Model.Transaction.Converters;

namespace NewTransactionModel.Model.Transaction;

[JsonConverter(typeof(AbstractTransactionConverter))]
public abstract record AbstractTransaction(
    TransactionId TransactionId, 
    Guid PayloadKind, 
    Timestamp TransactionTimeStamp)
{

    public abstract bool CheckSignature();

}
using System.Text.Json.Serialization;
using HushNetwork.Model.Transaction.Converters;

namespace HushNetwork.Model.Transaction;

[JsonConverter(typeof(TransactionIdConverter))]
public readonly record struct  TransactionId(Guid Value)
{
    public static TransactionId Empty { get; } = new(Guid.Empty);
    public static TransactionId NewTransactionId { get; } = new(Guid.NewGuid());

    public override string ToString() => this.Value.ToString();
}



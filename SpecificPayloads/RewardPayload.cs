using NewTransactionModel.Model.Transaction;

namespace NewTransactionModel.SpecificPayloads;

public record RewardPayload(string Token, string Amount) : TransactionPayloadKind;

public static class RewardPayloadHandler
{
    public static Guid RewardPayloadKind = Guid.Parse("e054b791-5e99-41aa-b870-a7201bc85ec3");
}

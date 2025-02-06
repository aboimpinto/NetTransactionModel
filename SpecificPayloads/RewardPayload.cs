using NewTransactionModel.Model.Transaction;

namespace NewTransactionModel.SpecificPayloads;

public record RewardPayload(string Token, string Amount) : TransactionPayloadKind;



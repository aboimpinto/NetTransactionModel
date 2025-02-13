using HushNetwork.Model.Transaction;

namespace NewTransactionModel.SpecificPayloads;

public record EmptyPayload : ITransactionPayloadKind;

public static class EmptyPayloadHandler
{
    public static Guid EmptyPayloadKind = Guid.Parse("af4be999-c8d1-499d-ad18-baa865fd90ff");
}
using System.Text.Json;
using System.Text.Json.Serialization;
using NewTransactionModel;
using NewTransactionModel.Model;
using NewTransactionModel.Model.Transaction;
using NewTransactionModel.Model.Transaction.Signed;
using NewTransactionModel.Model.Transaction.Unsigned;

// Step 3: Validator validates the transaction
public record ValidatedTransaction<T>: SignedTransaction<T>
    where T: ITransactionPayloadKind
{
    public SignatureInfo ValidatorSignature { get; init; }

    public ValidatedTransaction(
        SignedTransaction<T> signedTransaction, 
        SignatureInfo validatorSignature) 
        : base(
            signedTransaction, 
            signedTransaction.UserSignature)
    {
        this.ValidatorSignature = validatorSignature;
    }

    [JsonConstructor]
    public ValidatedTransaction(
        TransactionId TransactionId,
        Guid PayloadKind,
        Timestamp TransactionTimeStamp,
        T Payload,
        long PayloadSize,
        SignatureInfo UserSignature,
        SignatureInfo ValidatorSignature)
        : base(
            new SignedTransaction<T>(
                new UnsignedTransaction<T>(
                    TransactionId,
                    PayloadKind,
                    TransactionTimeStamp,
                    Payload,
                    PayloadSize), 
                UserSignature),
            UserSignature)
    {
        this.ValidatorSignature = ValidatorSignature;
    }

    public override bool CheckSignature()
    {
        var signedTransaction = new SignedTransaction<T>(
            new UnsignedTransaction<T>(
                this.TransactionId, 
                this.PayloadKind,
                this.TransactionTimeStamp, 
                this.Payload,
                this.PayloadSize), 
            this.UserSignature);

        var isValidatorSignatureSignatureValid = SigningKeys.VerifySignature(
            JsonSerializer.Serialize(signedTransaction), 
            this.ValidatorSignature.Signature, 
            this.ValidatorSignature.Signatory);

        var usignedTransaction = new UnsignedTransaction<T>(
            this.TransactionId, 
                this.PayloadKind,
                this.TransactionTimeStamp, 
                this.Payload,
                this.PayloadSize);

        var isUserSignatureSignatureValid = SigningKeys.VerifySignature(
            JsonSerializer.Serialize(usignedTransaction), 
            this.UserSignature.Signature, 
            this.UserSignature.Signatory);

        return isValidatorSignatureSignatureValid && isUserSignatureSignatureValid;
    }
}
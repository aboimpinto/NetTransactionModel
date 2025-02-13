I've reviewed the code, and here are my observations:

SignedBlock.cs:

The SignedBlock class is correctly implemented as an immutable record with two properties: BlockProducerSignature and the inherited UnsignedBlock properties.
The constructor that takes an UnsignedBlock and a SignatureInfo is correct. However, it's unusual to have a separate property for the block producer signature when the same information is already stored in the UnsignedBlock instance.
UnsignedBlockHandler.cs:

The 
CreateGenesis
 method correctly creates a new SignedBlock with the Genesis Block ID and an empty list of transactions.
The 
CreateNew
 methods seem to be correctly implementing the creation of new blocks, both signed and unsigned. However, there's no validation for the block index, previous/next block IDs, or timestamp consistency.
The 
SignIt
 method is a nice addition to easily sign an existing UnsignedBlock.
SignedTransaction.cs:

This class is a simple immutable record with two properties: UserSignature and inherited UnsignedTransaction properties. It's correctly implementing the signing of transactions.
SignedTransactionHandler.cs:

The 
SignByValidator
 methods are correctly implementing the signing of transactions by a validator.
There's no validation for the signatures, though. A simple check could be added to ensure that the signatures match the expected format or hash.
UnsignedTransactionHandler.cs:

This class has two methods: 
CreateNew
 and 
SignByUser
. Both seem to be correctly implementing the creation of new transactions and signing them by a user.
There's no validation for the transaction payload or timestamp consistency.
ValidatedTransaction.cs (both versions):

The classes are correctly implementing immutable records with four properties: TransactionId, PayloadKind, TransactionTimeStamp, and Payload. They're also correctly inheriting from SignedTransaction.
The constructors seem to be correctly initializing the properties.
However, there's no validation for the transaction payload or timestamp consistency.
Inconsistencies and suggestions for improvement:

In the ValidatedTransaction classes, you have two constructors. One is marked as [JsonConstructor], which suggests that it's intended for JSON deserialization. The other constructor takes a SignedTransaction instance and signs it again with the validator signature. This seems unnecessary, as the SignedTransaction instance should already be validated.
In the SignedBlock class, you have two constructors. One takes an UnsignedBlock and a SignatureInfo. The other takes multiple parameters (block ID, creation timestamp, block index, previous/next block IDs, and transactions). It's unclear why this second constructor is necessary or how it differs from the first one.
In the 
SignedTransactionHandler
 class, you have two methods that sign a transaction by a validator. One method takes a SignatureInfo as a parameter, while the other method takes public and private keys. It's unclear why both methods are needed or how they differ.
You should consider adding more validation for the block and transaction properties (e.g., ensuring that block indices are consistent, timestamps are in order, and transactions are properly signed).
Overall, your code seems to be correctly implementing a blockchain producer test application with signed blocks and transactions. However, there's room for improvement by adding more validation and simplifying some of the constructors and methods.
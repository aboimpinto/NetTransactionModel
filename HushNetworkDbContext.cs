using Microsoft.EntityFrameworkCore;
using NewTransactionModel.Model.Block;
using NewTransactionModel.Model.Block.Finalized;

namespace NewTransactionModel;

public class HushNetworkDbContext : DbContext
{
    public DbSet<Block> Blocks { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Database=NewHushNetworkDb;Username=HushNetworkDb_USER;Password=HushNetworkDb_PASSWORD");
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder
            .Entity<Block>()
            .ToTable("Blockchain_Block")
            .HasKey(x => x.BlockId);

        modelBuilder
            .Entity<Block>()
            .Property(x => x.BlockId)
            .HasConversion(x => x.ToString(), x => BlockIdHandler.CreateFromString(x))
            .HasColumnType("varchar(40)");

        modelBuilder
            .Entity<Block>()
            .Property(x => x.BlockIndex)
            .HasConversion(x => x.ToString(), x => new BlockIndex(long.Parse(x)))
            .HasColumnType("varchar(20)");

        modelBuilder
            .Entity<Block>()
            .Property(x => x.PreviousBlockId)
            .HasConversion(x => x.ToString(), x => BlockIdHandler.CreateFromString(x))
            .HasColumnType("varchar(40)");

        modelBuilder
            .Entity<Block>()
            .Property(x => x.NextBlockId)
            .HasConversion(x => x.ToString(), x => BlockIdHandler.CreateFromString(x))
            .HasColumnType("varchar(40)");
    }
}

public static class BlockHandler
{
    public static Block ToBlockEntity(this FinalizedBlock finalizedBlock) => 
        new(
            finalizedBlock.BlockId,
            finalizedBlock.BlockIndex,
            finalizedBlock.PreviousBlockId,
            finalizedBlock.NextBlockId,
            finalizedBlock.Hash,
            finalizedBlock.ToJson());
}

public record Block
{
    public BlockId BlockId { get; init; }
    public BlockIndex BlockIndex { get; init; }
    public BlockId PreviousBlockId { get; init; }
    public BlockId NextBlockId { get; init; }
    public string Hash { get; init; }
    public string JsonValue { get; init; }

    public Block(
        BlockId BlockId, 
        BlockIndex BlockIndex,
        BlockId PreviousBlockId,
        BlockId NextBlockId,
        string Hash, 
        string JsonValue)
    {
        this.BlockId = BlockId;
        this.BlockIndex = BlockIndex;
        this.PreviousBlockId = PreviousBlockId;
        this.NextBlockId = NextBlockId;
        this.Hash = Hash;
        this.JsonValue = JsonValue;
    }
}




// dotnet ef migrations add InitialCreate
// dotnet ef database update
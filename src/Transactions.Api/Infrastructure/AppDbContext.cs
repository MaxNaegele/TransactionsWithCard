using Microsoft.EntityFrameworkCore;

public sealed class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions options) : base(options) { }
    public DbSet<CardTransaction> CardTransactions { get; set; }
    public DbSet<Parcel> Parcels { get; set; }
    public DbSet<Anticipation> Anticipations { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
       modelBuilder.Entity<CardTransaction>().Configure(); 
       modelBuilder.Entity<Parcel>().Configure(); 
       modelBuilder.Entity<Anticipation>().Configure(); 
    }
}
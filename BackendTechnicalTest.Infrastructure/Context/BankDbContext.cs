using BackendTechnicalTest.Models.Accounts;
using BackendTechnicalTest.Models.Clients;
using BackendTechnicalTest.Models.Transactions;
using Microsoft.EntityFrameworkCore;

namespace BackendTechnicalTest.Infrastructure.Context;

public class BankDbContext(DbContextOptions<BankDbContext> options ) : DbContext(options)
{
    
    public DbSet<Account> Accounts { get; set; }
    
    public DbSet<Client> Clients { get; set; }
    
    public DbSet<Transaction> Transactions { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Account>()
            .HasIndex(a => a.AccountNumber)
            .IsUnique();
        
        modelBuilder.Entity<Account>()
            .HasOne(a => a.Client)
            .WithMany(c => c.Accounts)
            .HasForeignKey(a => a.ClientId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Transaction>()
            .HasOne(t => t.Account)
            .WithMany(a => a.Transactions)
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.Cascade);
        
        modelBuilder.Entity<Client>()
            .Property(c => c.Gender)
            .HasConversion<string>();
        
        modelBuilder.Entity<Transaction>()
            .Property(t => t.Type)
            .HasConversion<string>();
    }
}
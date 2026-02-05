using Microsoft.EntityFrameworkCore;
using Features.Clients.Models;  
using Features.Users.Models;

namespace ClientServiceRazor.Data; 

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Client> Clients { get; set; }
    public DbSet<Phone> Phones { get; set; }
    public DbSet<Address> Addresses { get; set; }
    public DbSet<FinanceAccount> FinanceAccounts { get; set; }
    public DbSet<ClientFinanceAccount> ClientFinanceAccounts { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<Status> Statuses { get; set; }
    public DbSet<Role> Roles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>()
            .HasOne(c => c.Address)
            .WithOne()
            .HasForeignKey<Address>(a => a.ClientId);

        modelBuilder.Entity<Client>()
            .HasMany(c => c.Phones)
            .WithOne()  
            .HasForeignKey(p => p.ClientId);

        modelBuilder.Entity<ClientFinanceAccount>()
            .HasKey(cfa => new { cfa.ClientId, cfa.FinanceAccountId });
        
        modelBuilder.Entity<Client>()
            .HasIndex(c => c.Email)
            .IsUnique();
    }
}
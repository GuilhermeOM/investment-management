namespace InvestmentManagement.SharedDataContext;

using InvestmentManagement.SharedDataContext.Models;
using Microsoft.EntityFrameworkCore;

public class DataContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<ProductType> ProductTypes { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<CompanyProduct> CompanyProducts { get; set; }
    public DbSet<Client> Clients { get; set; }
    public DbSet<ClientProduct> ClientProducts { get; set; }
    public DbSet<Product> Products { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        _ = modelBuilder.Entity<ProductType>().HasIndex(u => u.Type).IsUnique();
        _ = modelBuilder.Entity<Company>().HasIndex(u => u.Email).IsUnique();
        _ = modelBuilder.Entity<Client>().HasIndex(u => u.Email).IsUnique();

        _ = modelBuilder.Entity<Product>()
            .HasOne(p => p.Company)
            .WithMany()
            .HasForeignKey(p => p.CompanyId);
        _ = modelBuilder.Entity<Product>()
            .HasOne(p => p.Type)
            .WithMany()
            .HasForeignKey(p => p.TypeId);

        _ = modelBuilder.Entity<ClientProduct>()
            .HasOne(cp => cp.Product)
            .WithMany()
            .HasForeignKey(cp => cp.ProductId);

        _ = modelBuilder.Entity<CompanyProduct>()
            .HasOne(cp => cp.Product)
            .WithMany()
            .HasForeignKey(cp => cp.ProductId);
    }
}

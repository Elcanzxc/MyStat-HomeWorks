using InvoiceProject.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InvoiceProject.DataAccess;

public class InvoiceDbContext : DbContext
{

    public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options)
        : base(options){  }


    public DbSet<CustomerEntity> Customers => Set<CustomerEntity>();
    public DbSet<InvoiceEntity> Invoices => Set<InvoiceEntity>();
    public DbSet<InvoiceRowEntity> InvoiceRows => Set<InvoiceRowEntity>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var entries = ChangeTracker
            .Entries()
            .Where(e => e.State is EntityState.Added or EntityState.Modified);

        foreach (var entityEntry in entries)
        {
        
            if (entityEntry.Entity is CustomerEntity || entityEntry.Entity is InvoiceEntity)
            {
             
                ((dynamic)entityEntry.Entity).UpdatedAt = DateTimeOffset.UtcNow;

                if (entityEntry.State == EntityState.Added)
                {
                    ((dynamic)entityEntry.Entity).CreatedAt = DateTimeOffset.UtcNow;
                }
            }
        }

        return base.SaveChangesAsync(cancellationToken);
    }
}

using InvoiceProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace InvoiceProject.DataAccess;

public class InvoiceDbContext : IdentityDbContext<User>
{

    public InvoiceDbContext(DbContextOptions<InvoiceDbContext> options)
        : base(options){  }


    public DbSet<Customer> Customers => Set<Customer>();
    public DbSet<Invoice> Invoices => Set<Invoice>();
    public DbSet<InvoiceRow> InvoiceRows => Set<InvoiceRow>();

    public DbSet<InvoiceAttachment> Attachments => Set<InvoiceAttachment>();



    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }


}

using InvoiceProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace InvoiceProject.DataAccess.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{

    public void Configure(EntityTypeBuilder<Customer> builder)
    {

    builder.HasKey(c => c.Id);

    builder.Property(c => c.Name).IsRequired().HasMaxLength(200);

    builder.Property(c => c.Email).IsRequired().HasMaxLength(150);


    builder.HasQueryFilter(c => c.DeletedAt == null);
    }
}

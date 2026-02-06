using InvoiceProject.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceProject.DataAccess.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<CustomerEntity>
{

    public void Configure(EntityTypeBuilder<CustomerEntity> builder)
    {
    builder.HasKey(c => c.Id);
    builder.Property(c => c.Name).IsRequired().HasMaxLength(200);
    builder.Property(c => c.Email).IsRequired().HasMaxLength(150);


    builder.HasQueryFilter(c => c.DeletedAt == null);

    builder.HasMany(c => c.Invoices)
           .WithOne(i => i.Customer)
           .HasForeignKey(i => i.CustomerId)
           .OnDelete(DeleteBehavior.Restrict); 
    }
}

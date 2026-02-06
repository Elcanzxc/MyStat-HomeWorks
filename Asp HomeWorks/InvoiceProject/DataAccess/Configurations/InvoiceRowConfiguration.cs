using InvoiceProject.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceProject.DataAccess.Configurations;

public class InvoiceRowConfiguration : IEntityTypeConfiguration<InvoiceRowEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceRowEntity> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Service).IsRequired().HasMaxLength(500);

        builder.Property(r => r.Quantity).HasPrecision(18, 4);
        builder.Property(r => r.Rate).HasPrecision(18, 2);
        builder.Property(r => r.Sum).HasPrecision(18, 2);
    }
}

using InvoiceProject.DataAccess.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceProject.DataAccess.Configurations;

public class InvoiceConfiguration : IEntityTypeConfiguration<InvoiceEntity>
{
    public void Configure(EntityTypeBuilder<InvoiceEntity> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.TotalSum)
               .HasPrecision(18, 2);

        builder.HasQueryFilter(i => i.DeletedAt == null);

       
        builder.HasMany(i => i.Rows)
               .WithOne(r => r.Invoice)
               .HasForeignKey(r => r.InvoiceId)
               .OnDelete(DeleteBehavior.Cascade);
    }
}

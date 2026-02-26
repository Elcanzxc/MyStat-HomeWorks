using InvoiceProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceProject.DataAccess.Configurations;

public class InvoiceRowConfiguration : IEntityTypeConfiguration<InvoiceRow>
{
    public void Configure(EntityTypeBuilder<InvoiceRow> builder)
    {
        builder.HasKey(r => r.Id);

        builder.Property(r => r.Service).IsRequired().HasMaxLength(500);

        builder.Property(r => r.Quantity).HasPrecision(18, 4);
        builder.Property(r => r.Rate).HasPrecision(18, 2);
        builder.Ignore(ir => ir.Sum);

        builder.HasOne(ir => ir.Invoice)
            .WithMany(i => i.Rows)
            .HasForeignKey(ir => ir.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);


    }
}

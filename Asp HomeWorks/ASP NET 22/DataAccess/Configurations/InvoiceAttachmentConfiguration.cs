using InvoiceProject.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace InvoiceProject.DataAccess.Configurations;
public class InvoiceAttachmentConfiguration : IEntityTypeConfiguration<InvoiceAttachment>
{
    public void Configure(EntityTypeBuilder<InvoiceAttachment> builder)
    {

        builder.HasKey(ta => ta.Id);

        builder.Property(ta => ta.OriginalFileName)
            .IsRequired()
            .HasMaxLength(500);

        builder.Property(ta => ta.StoredFileName)
             .IsRequired()
             .HasMaxLength(500);


        builder.Property(ta => ta.ContentType)
            .IsRequired()
             .HasMaxLength(200);


        builder.Property(ta => ta.UploadedByCustomerId)
              .IsRequired()
              .HasMaxLength(450);

        builder.HasOne(ta => ta.Invoice)
            .WithMany(t => t.Attachments)
            .HasForeignKey(ta => ta.InvoiceId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne(ta => ta.UploadedByCustomer)
            .WithMany()
            .HasForeignKey(ta => ta.UploadedByCustomerId)
            .OnDelete(DeleteBehavior.Restrict);
    }   
}

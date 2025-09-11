

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Entitites;

namespace MyRestaurant.Data.Configurations;

public class CustomerTypeConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder
            .HasKey(customer => customer.Id);

        builder
            .Property(customer => customer.FullName)
            .IsRequired()
            .HasColumnType("NVARCHAR(100)");

        builder.ToTable("Customers", table =>
        {
            table.HasCheckConstraint("CK_Customers_FullName_MinLength", "LEN(FullName) >= 2");
            table.HasCheckConstraint("CK_Customers_PhoneNumber_MustContainPlus", "[PhoneNumber] LIKE '+%'");
        });

        builder
            .Property(customer => customer.PhoneNumber)
            .HasMaxLength(35);

        builder
            .HasIndex(customer => customer.PhoneNumber)
            .IsUnique();


        builder
            .Property(customer => customer.RegistrationDate)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();




    }
}

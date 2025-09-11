using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Entitites;

namespace MyRestaurant.Data.Configurations;

public class WaiterTypeConfiguration : IEntityTypeConfiguration<Waiter>
{

    public void Configure(EntityTypeBuilder<Waiter> builder)
    {


        builder
            .HasKey(waiter => waiter.Id);


        builder
           .Property(waiter => waiter.FullName)
           .IsRequired()
           .HasColumnType("NVARCHAR(100)");

        builder.ToTable("Waiters", table =>
        {
            table.HasCheckConstraint("CK_Customers_FullName_MinLength", "LEN(FullName) >= 2");
        });


        builder
           .Property(waiter => waiter.HireDate)
           .HasDefaultValueSql("GETDATE()")
           .ValueGeneratedOnAdd();

        builder
            .Property(waiter => waiter.Salary)
            .IsRequired()
            .HasColumnType("DECIMAL(10, 2)");

    }
}

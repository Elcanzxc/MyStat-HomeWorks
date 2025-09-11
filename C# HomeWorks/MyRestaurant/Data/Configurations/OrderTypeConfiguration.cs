using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Entitites;

namespace MyRestaurant.Data.Configurations;

public class OrderTypeConfiguration : IEntityTypeConfiguration<Order>
{

    public void Configure(EntityTypeBuilder<Order> builder)
    {


        builder
            .HasKey(order => order.Id);


        builder
            .Property(order => order.OrderTime)
            .HasDefaultValueSql("GETDATE()")
            .ValueGeneratedOnAdd();

        builder
            .Property(order => order.Status)
            .HasMaxLength(20)
            .HasDefaultValue("In Progress");


        builder
            .Property(order => order.TotalPrice)
            .IsRequired()
            .HasColumnType("DECIMAL(10, 2)");



    }
}

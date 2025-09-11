using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Entitites;

namespace MyRestaurant.Data.Configurations;

public class OrderItemTypeConfiguration : IEntityTypeConfiguration<OrderItem>
{

    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {


        builder
            .HasKey(orderItem => orderItem.Id);

        builder
            .Property(orderItem => orderItem.Quantity)
            .IsRequired();

        builder.ToTable("OrderItems", table =>
        {
            table.HasCheckConstraint("CK_OrderItem_Quantity_Positive", "Quantity > 0");
        });

        builder
            .Property(orderItem => orderItem.PriceAtOrder)
            .IsRequired()
            .HasColumnType("DECIMAL(10, 2)");

        builder
            .HasOne(oi => oi.Order)
            .WithMany(o => o.OrderItems)
            .HasForeignKey(oi => oi.OrderId)
            .OnDelete(DeleteBehavior.Cascade);
 

    }
}

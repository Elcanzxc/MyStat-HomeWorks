using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyRestaurant.Entitites;

namespace MyRestaurant.Data.Configurations;

public class MenuTypeConfiguration : IEntityTypeConfiguration<Menu>
{

    public void Configure(EntityTypeBuilder<Menu> builder)
    {

        builder
            .HasKey(menu => menu.Id);


        builder
            .Property(menu => menu.Name)
            .IsRequired()
            .HasColumnType("NVARCHAR(40)");

        builder.ToTable("Menu", table =>
        {
            table.HasCheckConstraint("CK_Customers_Name_MinLength", "LEN(Name) >= 2");
        });


        builder
            .Property(menu => menu.Price)
            .IsRequired()
            .HasColumnType("DECIMAL(10, 2)");


        builder
            .Property(menu => menu.Category)
            .HasColumnType("NVARCHAR(20)");

        builder
            .Property(menu => menu.Description)
            .HasColumnType("NVARCHAR(255)");





    }
}

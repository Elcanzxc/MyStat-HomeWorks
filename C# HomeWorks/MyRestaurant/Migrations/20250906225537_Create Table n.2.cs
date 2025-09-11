using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyRestaurant.Migrations
{
    /// <inheritdoc />
    public partial class CreateTablen2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MenuItemId",
                table: "OrderItems");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MenuItemId",
                table: "OrderItems",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}

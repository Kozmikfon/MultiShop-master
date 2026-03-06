using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Order.Persistence.Migrations
{
    public partial class OrderAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                table: "Orderings",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AddressId",
                table: "Orderings");
        }
    }
}

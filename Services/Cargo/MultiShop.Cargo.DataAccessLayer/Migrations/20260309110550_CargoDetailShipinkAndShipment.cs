using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Cargo.DataAccessLayer.Migrations
{
    public partial class CargoDetailShipinkAndShipment : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShipinkOrderId",
                table: "CargoDetails",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShipinkShipmentId",
                table: "CargoDetails",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShipinkOrderId",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "ShipinkShipmentId",
                table: "CargoDetails");
        }
    }
}

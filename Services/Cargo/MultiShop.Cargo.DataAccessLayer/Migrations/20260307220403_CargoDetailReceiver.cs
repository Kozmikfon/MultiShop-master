using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Cargo.DataAccessLayer.Migrations
{
    public partial class CargoDetailReceiver : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReceiverAddressDetail",
                table: "CargoDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverCity",
                table: "CargoDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverDistrict",
                table: "CargoDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverEmail",
                table: "CargoDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverName",
                table: "CargoDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverPhone",
                table: "CargoDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ReceiverSurname",
                table: "CargoDetails",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReceiverAddressDetail",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "ReceiverCity",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "ReceiverDistrict",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "ReceiverEmail",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "ReceiverName",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "ReceiverPhone",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "ReceiverSurname",
                table: "CargoDetails");
        }
    }
}

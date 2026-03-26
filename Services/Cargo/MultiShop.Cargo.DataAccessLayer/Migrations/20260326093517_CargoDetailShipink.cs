using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Cargo.DataAccessLayer.Migrations
{
    public partial class CargoDetailShipink : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Height",
                table: "CargoDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Length",
                table: "CargoDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<double>(
                name: "Weight",
                table: "CargoDetails",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<int>(
                name: "Width",
                table: "CargoDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "CarrierAccountId",
                table: "CargoCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "CarrierServiceId",
                table: "CargoCompanies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Height",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "Length",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "Weight",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "Width",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "CarrierAccountId",
                table: "CargoCompanies");

            migrationBuilder.DropColumn(
                name: "CarrierServiceId",
                table: "CargoCompanies");
        }
    }
}

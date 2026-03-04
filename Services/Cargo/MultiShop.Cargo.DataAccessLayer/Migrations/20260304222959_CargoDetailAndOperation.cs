using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MultiShop.Cargo.DataAccessLayer.Migrations
{
    public partial class CargoDetailAndOperation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ReceiverCustomer",
                table: "CargoDetails",
                newName: "VendorId");

            migrationBuilder.AddColumn<int>(
                name: "CargoCustomerId",
                table: "CargoDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CurrentStatus",
                table: "CargoDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "OrderingId",
                table: "CargoDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_CargoDetails_CargoCustomerId",
                table: "CargoDetails",
                column: "CargoCustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_CargoDetails_CargoCustomers_CargoCustomerId",
                table: "CargoDetails",
                column: "CargoCustomerId",
                principalTable: "CargoCustomers",
                principalColumn: "CargoCustomerId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CargoDetails_CargoCustomers_CargoCustomerId",
                table: "CargoDetails");

            migrationBuilder.DropIndex(
                name: "IX_CargoDetails_CargoCustomerId",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "CargoCustomerId",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "CurrentStatus",
                table: "CargoDetails");

            migrationBuilder.DropColumn(
                name: "OrderingId",
                table: "CargoDetails");

            migrationBuilder.RenameColumn(
                name: "VendorId",
                table: "CargoDetails",
                newName: "ReceiverCustomer");
        }
    }
}

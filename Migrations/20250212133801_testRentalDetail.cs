using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VideoshopAPIV3.Migrations
{
    /// <inheritdoc />
    public partial class testRentalDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RentalDetailId",
                table: "Customers",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Customers_RentalDetailId",
                table: "Customers",
                column: "RentalDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_RentalDetails_RentalDetailId",
                table: "Customers",
                column: "RentalDetailId",
                principalTable: "RentalDetails",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Customers_RentalDetails_RentalDetailId",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_RentalDetailId",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "RentalDetailId",
                table: "Customers");
        }
    }
}

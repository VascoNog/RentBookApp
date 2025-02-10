using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentBookApp.Migrations
{
    /// <inheritdoc />
    public partial class AddNewPropertyEntityRental_RenterIdIdentityUserRenter_FKUserId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RenterId",
                table: "Rentals",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_RenterId",
                table: "Rentals",
                column: "RenterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Rentals_AspNetUsers_RenterId",
                table: "Rentals",
                column: "RenterId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Rentals_AspNetUsers_RenterId",
                table: "Rentals");

            migrationBuilder.DropIndex(
                name: "IX_Rentals_RenterId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "RenterId",
                table: "Rentals");
        }
    }
}

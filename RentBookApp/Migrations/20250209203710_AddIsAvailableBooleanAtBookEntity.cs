using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RentBookApp.Migrations
{
    /// <inheritdoc />
    public partial class AddIsAvailableBooleanAtBookEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Book",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Book");
        }
    }
}

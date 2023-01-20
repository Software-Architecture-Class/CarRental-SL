using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration11 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageTitle",
                table: "Cars",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageTitle",
                table: "Cars");
        }
    }
}

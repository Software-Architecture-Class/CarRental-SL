using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRentalServiceAPI.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration10 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AuthenticationCredentials",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PasswordHash = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    PasswordSalt = table.Column<byte[]>(type: "varbinary(max)", nullable: false),
                    Token = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuthenticationCredentials", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "CarEvents",
                columns: table => new
                {
                    TransactionId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CarId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    startDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    finishDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    paidPrice = table.Column<double>(type: "float", nullable: false),
                    Rate = table.Column<int>(type: "int", nullable: false),
                    DiscountPercentage = table.Column<double>(type: "float", nullable: false),
                    Discount = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    rentType = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CarEvents", x => x.TransactionId);
                });

            migrationBuilder.CreateTable(
                name: "Cars",
                columns: table => new
                {
                    CarId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Power = table.Column<int>(type: "int", nullable: false),
                    Acceleration = table.Column<int>(type: "int", nullable: false),
                    gearboxType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Drive = table.Column<int>(type: "int", nullable: false),
                    carCategory = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Price = table.Column<double>(type: "float", nullable: false),
                    peopleCapacity = table.Column<int>(type: "int", nullable: false),
                    Image = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<int>(type: "int", nullable: false),
                    Popularity = table.Column<int>(type: "int", nullable: false),
                    LastTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cars", x => x.CarId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CardNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    userType = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastTimeModified = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AuthenticationCredentials");

            migrationBuilder.DropTable(
                name: "CarEvents");

            migrationBuilder.DropTable(
                name: "Cars");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}

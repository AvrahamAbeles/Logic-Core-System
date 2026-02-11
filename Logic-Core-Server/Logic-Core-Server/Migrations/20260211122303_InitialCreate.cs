using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Logic_Core_Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CalculationLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    OperationKey = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InputA = table.Column<double>(type: "float", nullable: false),
                    InputB = table.Column<double>(type: "float", nullable: false),
                    Result = table.Column<double>(type: "float", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalculationLogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Operations",
                columns: table => new
                {
                    Key = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Formula = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Operations", x => x.Key);
                });

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Key", "Formula", "IsActive", "Name" },
                values: new object[,]
                {
                    { "add", "arg1 + arg2", true, "חיבור (+)" },
                    { "div", "arg1 / arg2", true, "חילוק (/)" },
                    { "mul", "arg1 * arg2", true, "כפל (*)" },
                    { "sub", "arg1 - arg2", true, "חיסור (-)" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CalculationLogs");

            migrationBuilder.DropTable(
                name: "Operations");
        }
    }
}

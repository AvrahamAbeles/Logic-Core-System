using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic_Core_Server.Migrations
{
    /// <inheritdoc />
    public partial class SeedTaxOperation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Key", "Formula", "IsActive", "Name", "Symbol", "ValidationMessage", "ValidationRegex" },
                values: new object[] { "tax", "arg1 * 0.18", true, "חישוב כולל מס", "Tax %", "פעולה זו תומכת במספרים בלבד", "^-?\\d+(\\.\\d+)?$" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "tax");
        }
    }
}

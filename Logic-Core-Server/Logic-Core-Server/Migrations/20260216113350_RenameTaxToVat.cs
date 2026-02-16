using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic_Core_Server.Migrations
{
    /// <inheritdoc />
    public partial class RenameTaxToVat : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "tax");

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Key", "Formula", "IsActive", "Name", "Symbol", "ValidationMessage", "ValidationRegex" },
                values: new object[] { "vat", "arg1 * 1.18", true, "חישוב כולל מעמ", "vat %", "פעולה זו תומכת במספרים בלבד", "^-?\\d+(\\.\\d+)?$" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "vat");

            migrationBuilder.InsertData(
                table: "Operations",
                columns: new[] { "Key", "Formula", "IsActive", "Name", "Symbol", "ValidationMessage", "ValidationRegex" },
                values: new object[] { "tax", "arg1 * 1.18", true, "חישוב כולל מעמ", "Tax %", "פעולה זו תומכת במספרים בלבד", "^-?\\d+(\\.\\d+)?$" });
        }
    }
}

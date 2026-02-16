using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic_Core_Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVatFormula : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "tax",
                columns: new[] { "Formula", "Name" },
                values: new object[] { "arg1 * 1.18", "חישוב כולל מעמ" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "tax",
                columns: new[] { "Formula", "Name" },
                values: new object[] { "arg1 * 0.18", "חישוב כולל מס" });
        }
    }
}

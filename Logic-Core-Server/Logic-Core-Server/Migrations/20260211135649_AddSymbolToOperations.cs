using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic_Core_Server.Migrations
{
    /// <inheritdoc />
    public partial class AddSymbolToOperations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Symbol",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "add",
                columns: new[] { "Name", "Symbol" },
                values: new object[] { "חיבור", "+" });

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "div",
                columns: new[] { "Name", "Symbol" },
                values: new object[] { "חילוק", "/" });

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "mul",
                columns: new[] { "Name", "Symbol" },
                values: new object[] { "כפל", "*" });

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "sub",
                columns: new[] { "Name", "Symbol" },
                values: new object[] { "חיסור", "-" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Symbol",
                table: "Operations");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "add",
                column: "Name",
                value: "חיבור (+)");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "div",
                column: "Name",
                value: "חילוק (/)");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "mul",
                column: "Name",
                value: "כפל (*)");

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "sub",
                column: "Name",
                value: "חיסור (-)");
        }
    }
}

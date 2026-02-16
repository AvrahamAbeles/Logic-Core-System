using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic_Core_Server.Migrations
{
    /// <inheritdoc />
    public partial class AddValidationToOperations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ValidationMessage",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ValidationRegex",
                table: "Operations",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "add",
                columns: new[] { "ValidationMessage", "ValidationRegex" },
                values: new object[] { null, null });

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "div",
                columns: new[] { "ValidationMessage", "ValidationRegex" },
                values: new object[] { "פעולה זו תומכת במספרים בלבד", "^-?\\d+(\\.\\d+)?$" });

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "mul",
                columns: new[] { "ValidationMessage", "ValidationRegex" },
                values: new object[] { "פעולה זו תומכת במספרים בלבד", "^-?\\d+(\\.\\d+)?$" });

            migrationBuilder.UpdateData(
                table: "Operations",
                keyColumn: "Key",
                keyValue: "sub",
                columns: new[] { "ValidationMessage", "ValidationRegex" },
                values: new object[] { "פעולה זו תומכת במספרים בלבד", "^-?\\d+(\\.\\d+)?$" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ValidationMessage",
                table: "Operations");

            migrationBuilder.DropColumn(
                name: "ValidationRegex",
                table: "Operations");
        }
    }
}

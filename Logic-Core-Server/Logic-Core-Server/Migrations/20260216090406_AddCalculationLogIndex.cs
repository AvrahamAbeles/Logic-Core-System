using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Logic_Core_Server.Migrations
{
    /// <inheritdoc />
    public partial class AddCalculationLogIndex : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "OperationKey",
                table: "CalculationLogs",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CalculationLogs_OperationKey_CreatedAt",
                table: "CalculationLogs",
                columns: new[] { "OperationKey", "CreatedAt" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_CalculationLogs_OperationKey_CreatedAt",
                table: "CalculationLogs");

            migrationBuilder.AlterColumn<string>(
                name: "OperationKey",
                table: "CalculationLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}

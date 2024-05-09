using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AddConverterFromJsonForUserAndGroup : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StudentGroups",
                keyColumn: "Id",
                keyValue: 1,
                column: "Number",
                value: 20);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "StudentGroups",
                keyColumn: "Id",
                keyValue: 1,
                column: "Number",
                value: 0);
        }
    }
}

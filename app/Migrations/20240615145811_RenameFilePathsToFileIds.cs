using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class RenameFilePathsToFileIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileLink",
                table: "HomeworkFiles",
                newName: "FileId");

            migrationBuilder.RenameIndex(
                name: "IX_HomeworkFiles_FileLink",
                table: "HomeworkFiles",
                newName: "IX_HomeworkFiles_FileId");

            migrationBuilder.UpdateData(
                table: "StudentAbsence",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "StudentAbsence",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "StudentAbsence",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 6, 15, 0, 0, 0, 0, DateTimeKind.Local));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FileId",
                table: "HomeworkFiles",
                newName: "FileLink");

            migrationBuilder.RenameIndex(
                name: "IX_HomeworkFiles_FileId",
                table: "HomeworkFiles",
                newName: "IX_HomeworkFiles_FileLink");

            migrationBuilder.UpdateData(
                table: "StudentAbsence",
                keyColumn: "Id",
                keyValue: 1,
                column: "Date",
                value: new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "StudentAbsence",
                keyColumn: "Id",
                keyValue: 2,
                column: "Date",
                value: new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Local));

            migrationBuilder.UpdateData(
                table: "StudentAbsence",
                keyColumn: "Id",
                keyValue: 3,
                column: "Date",
                value: new DateTime(2024, 6, 14, 0, 0, 0, 0, DateTimeKind.Local));
        }
    }
}

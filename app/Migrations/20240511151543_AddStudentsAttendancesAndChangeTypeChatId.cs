using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class AddStudentsAttendancesAndChangeTypeChatId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "IdChat",
                table: "Users",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "StudentAttendances",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<long>(type: "bigint", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    MissedFirstLesson = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MissedSecondLesson = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MissedThirdLesson = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MissedFourthLesson = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MissedFifthLesson = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    MissedSixthLesson = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAttendances", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "StudentAttendances",
                columns: new[] { "Id", "Date", "MissedFifthLesson", "MissedFirstLesson", "MissedFourthLesson", "MissedSecondLesson", "MissedSixthLesson", "MissedThirdLesson", "StudentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), false, true, false, false, false, false, 1L },
                    { 2, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), false, true, false, true, false, false, 2L }
                });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdChat", "Patronymic", "Surname" },
                values: new object[] { 597239235L, "Микаилович", "Микаилов" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IdChat", "Patronymic" },
                values: new object[] { 597239236L, "Алданович" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdChat", "Patronymic", "Surname" },
                values: new object[] { 597239238L, "Магомедович", "Магомедов" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IdChat", "Name", "Patronymic", "Surname" },
                values: new object[] { 597239239L, "Дер", "Дерович", "Деров" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IdChat", "Name", "Patronymic", "Surname" },
                values: new object[] { 597239240L, "Кер", "Керович", "Керов" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "StudentAttendances");

            migrationBuilder.AlterColumn<int>(
                name: "IdChat",
                table: "Users",
                type: "int",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "IdChat", "Patronymic", "Surname" },
                values: new object[] { 597239235, "Альбертович", "Османов" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "IdChat", "Patronymic" },
                values: new object[] { 597239236, "Маратович" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "IdChat", "Patronymic", "Surname" },
                values: new object[] { 597239238, "Магомедов", "Аккаев" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "IdChat", "Name", "Patronymic", "Surname" },
                values: new object[] { 597239239, "Султан", "Ахмедович", "Шихмарданов" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "IdChat", "Name", "Patronymic", "Surname" },
                values: new object[] { 597239240, "Саим", "Альбертович", "Саидов" });
        }
    }
}

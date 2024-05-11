using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAttendanceFields : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MissedFifthLesson",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "MissedFirstLesson",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "MissedFourthLesson",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "MissedSecondLesson",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "MissedSixthLesson",
                table: "StudentAttendances");

            migrationBuilder.DropColumn(
                name: "MissedThirdLesson",
                table: "StudentAttendances");

            migrationBuilder.AddColumn<byte>(
                name: "NumberLesson",
                table: "StudentAttendances",
                type: "tinyint unsigned",
                nullable: false,
                defaultValue: (byte)0);

            migrationBuilder.UpdateData(
                table: "StudentAttendances",
                keyColumn: "Id",
                keyValue: 1,
                column: "NumberLesson",
                value: (byte)1);

            migrationBuilder.UpdateData(
                table: "StudentAttendances",
                keyColumn: "Id",
                keyValue: 2,
                column: "NumberLesson",
                value: (byte)2);

            migrationBuilder.InsertData(
                table: "StudentAttendances",
                columns: new[] { "Id", "Date", "NumberLesson", "StudentId" },
                values: new object[] { 3, new DateTime(2024, 5, 11, 0, 0, 0, 0, DateTimeKind.Local), (byte)2, 3L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "StudentAttendances",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DropColumn(
                name: "NumberLesson",
                table: "StudentAttendances");

            migrationBuilder.AddColumn<bool>(
                name: "MissedFifthLesson",
                table: "StudentAttendances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MissedFirstLesson",
                table: "StudentAttendances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MissedFourthLesson",
                table: "StudentAttendances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MissedSecondLesson",
                table: "StudentAttendances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MissedSixthLesson",
                table: "StudentAttendances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "MissedThirdLesson",
                table: "StudentAttendances",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "StudentAttendances",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "MissedFifthLesson", "MissedFirstLesson", "MissedFourthLesson", "MissedSecondLesson", "MissedSixthLesson", "MissedThirdLesson" },
                values: new object[] { false, true, false, false, false, false });

            migrationBuilder.UpdateData(
                table: "StudentAttendances",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "MissedFifthLesson", "MissedFirstLesson", "MissedFourthLesson", "MissedSecondLesson", "MissedSixthLesson", "MissedThirdLesson" },
                values: new object[] { false, true, false, true, false, false });
        }
    }
}

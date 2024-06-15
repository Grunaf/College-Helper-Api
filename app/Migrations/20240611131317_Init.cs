using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace app.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Professors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsCurator = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    IdChat = table.Column<long>(type: "bigint", nullable: false),
                    Surname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Patronymic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Professors", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Title = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IsExpired = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Field = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CuratorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroups", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentGroups_Professors_CuratorId",
                        column: x => x.CuratorId,
                        principalTable: "Professors",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SheduleDays",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentGroupId = table.Column<int>(type: "int", nullable: false),
                    CountWeek = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    CountDay = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheduleDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SheduleDays_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IsHeadBoy = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    ChatId = table.Column<long>(type: "bigint", nullable: false),
                    Surname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Patronymic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentGroupId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Students_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SheduleDaySubjects",
                columns: table => new
                {
                    SheduleDayId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Spot = table.Column<byte>(type: "tinyint unsigned", nullable: false),
                    SubgroupSequence = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SheduleDaySubjects", x => new { x.SheduleDayId, x.SubjectId, x.Spot });
                    table.ForeignKey(
                        name: "FK_SheduleDaySubjects_SheduleDays_SheduleDayId",
                        column: x => x.SheduleDayId,
                        principalTable: "SheduleDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SheduleDaySubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentAbsence",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    LessonNumber = table.Column<byte>(type: "tinyint unsigned", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentAbsence", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StudentAbsence_Students_StudentId",
                        column: x => x.StudentId,
                        principalTable: "Students",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "StudentGroups",
                columns: new[] { "Id", "CuratorId", "Field", "Number" },
                values: new object[,]
                {
                    { 1, null, "ИСП", 20 },
                    { 2, null, "ИСП", 21 }
                });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "IsExpired", "Title" },
                values: new object[,]
                {
                    { 1, false, "МДК 01.01" },
                    { 2, false, "МДК 01.03" },
                    { 3, false, "МДК 01.04" },
                    { 4, false, "МДК 01.05" },
                    { 5, false, "МДК 11.01" },
                    { 6, false, "МДК 01.01" },
                    { 7, false, "МДК 01.03" },
                    { 8, false, "МДК 01.04" },
                    { 9, false, "МДК 01.05" },
                    { 10, false, "МДК 11.01" },
                    { 11, false, "Физ-ра" },
                    { 12, false, "Иностранный язык" },
                    { 13, false, "Безопасность Жизндеятельности" },
                    { 14, false, "Экономика отросли" }
                });

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "ChatId", "IsHeadBoy", "Name", "Patronymic", "StudentGroupId", "Surname" },
                values: new object[,]
                {
                    { 1, 597239235L, true, "Микаил", "Микаилович", 1, "Микаилов" },
                    { 2, 597239236L, false, "Алдан", "Алданович", 1, "Омардибиров" },
                    { 3, 597239237L, false, "Магомед", "Магомедович", 1, "Магомедов" },
                    { 4, 6418436193L, true, "Дер", "Дерович", 2, "Деров" },
                    { 5, 597239249L, false, "Кер", "Керович", 2, "Керов" },
                    { 6, 597239239L, false, "Вер", "Верович", 2, "Веров" }
                });

            migrationBuilder.InsertData(
                table: "StudentAbsence",
                columns: new[] { "Id", "Date", "LessonNumber", "StudentId" },
                values: new object[,]
                {
                    { 1, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), (byte)1, 1 },
                    { 2, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), (byte)2, 2 },
                    { 3, new DateTime(2024, 6, 11, 0, 0, 0, 0, DateTimeKind.Local), (byte)2, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_SheduleDays_StudentGroupId",
                table: "SheduleDays",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_SheduleDaySubjects_SubjectId",
                table: "SheduleDaySubjects",
                column: "SubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentAbsence_StudentId",
                table: "StudentAbsence",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_CuratorId",
                table: "StudentGroups",
                column: "CuratorId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_ChatId",
                table: "Students",
                column: "ChatId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Students_StudentGroupId",
                table: "Students",
                column: "StudentGroupId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SheduleDaySubjects");

            migrationBuilder.DropTable(
                name: "StudentAbsence");

            migrationBuilder.DropTable(
                name: "SheduleDays");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "StudentGroups");

            migrationBuilder.DropTable(
                name: "Professors");
        }
    }
}

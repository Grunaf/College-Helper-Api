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
                        .Annotation("MySql:CharSet", "utf8mb4")
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
                name: "Homeworks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    StudentGroupId = table.Column<int>(type: "int", nullable: false),
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Comment = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    CreatedTime = table.Column<DateTime>(type: "datetime(6)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Homeworks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Homeworks_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Homeworks_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                name: "StudentGroupSubjects",
                columns: table => new
                {
                    SubjectId = table.Column<int>(type: "int", nullable: false),
                    StudentGroupId = table.Column<int>(type: "int", nullable: false),
                    IsExpired = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroupSubjects", x => new { x.StudentGroupId, x.SubjectId });
                    table.ForeignKey(
                        name: "FK_StudentGroupSubjects_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StudentGroupSubjects_Subjects_SubjectId",
                        column: x => x.SubjectId,
                        principalTable: "Subjects",
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
                name: "HomeworkFiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    HomeworkId = table.Column<int>(type: "int", nullable: false),
                    FileId = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HomeworkFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_HomeworkFiles_Homeworks_HomeworkId",
                        column: x => x.HomeworkId,
                        principalTable: "Homeworks",
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
                    Subgroup = table.Column<string>(type: "varchar(1)", nullable: true)
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
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                columns: new[] { "Id", "Title" },
                values: new object[,]
                {
                    { 1, "МДК 01.01" },
                    { 2, "МДК 01.03" },
                    { 3, "МДК 01.04" },
                    { 4, "МДК 01.05" },
                    { 5, "МДК 11.01" },
                    { 6, "МДК 01.01" },
                    { 7, "МДК 01.03" },
                    { 8, "МДК 01.04" },
                    { 9, "МДК 01.05" },
                    { 10, "МДК 11.01" },
                    { 11, "Физ-ра" },
                    { 12, "Иностранный язык" },
                    { 13, "Безопасность Жизндеятельности" },
                    { 14, "Экономика отросли" }
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
                    { 1, new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Local), (byte)1, 1 },
                    { 2, new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Local), (byte)2, 2 },
                    { 3, new DateTime(2024, 6, 26, 0, 0, 0, 0, DateTimeKind.Local), (byte)2, 4 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkFiles_FileId",
                table: "HomeworkFiles",
                column: "FileId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_HomeworkFiles_HomeworkId",
                table: "HomeworkFiles",
                column: "HomeworkId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_StudentGroupId",
                table: "Homeworks",
                column: "StudentGroupId");

            migrationBuilder.CreateIndex(
                name: "IX_Homeworks_SubjectId",
                table: "Homeworks",
                column: "SubjectId");

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
                name: "IX_StudentGroupSubjects_SubjectId",
                table: "StudentGroupSubjects",
                column: "SubjectId");

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
                name: "HomeworkFiles");

            migrationBuilder.DropTable(
                name: "SheduleDaySubjects");

            migrationBuilder.DropTable(
                name: "StudentAbsence");

            migrationBuilder.DropTable(
                name: "StudentGroupSubjects");

            migrationBuilder.DropTable(
                name: "Homeworks");

            migrationBuilder.DropTable(
                name: "SheduleDays");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "StudentGroups");

            migrationBuilder.DropTable(
                name: "Professors");
        }
    }
}

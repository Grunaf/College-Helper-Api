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
                name: "Subjects",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TypeSubject = table.Column<int>(type: "int", nullable: false),
                    isExpired = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Subjects", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Shedule",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CountWeek = table.Column<short>(type: "smallint", nullable: false),
                    FirstSubjectId = table.Column<int>(type: "int", nullable: true),
                    SecondSubjectId = table.Column<int>(type: "int", nullable: true),
                    ThirdSubjectId = table.Column<int>(type: "int", nullable: true),
                    FourthSubjectId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shedule", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Shedule_Subjects_FirstSubjectId",
                        column: x => x.FirstSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shedule_Subjects_FourthSubjectId",
                        column: x => x.FourthSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shedule_Subjects_SecondSubjectId",
                        column: x => x.SecondSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Shedule_Subjects_ThirdSubjectId",
                        column: x => x.ThirdSubjectId,
                        principalTable: "Subjects",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudentGroups",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Field = table.Column<int>(type: "int", nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false),
                    CuratorId = table.Column<int>(type: "int", nullable: true),
                    HeadBoyId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudentGroups", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Role = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    IdChat = table.Column<int>(type: "int", nullable: false),
                    Surname = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Patronymic = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    StudentGroupId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_StudentGroups_StudentGroupId",
                        column: x => x.StudentGroupId,
                        principalTable: "StudentGroups",
                        principalColumn: "Id");
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "StudentGroups",
                columns: new[] { "Id", "CuratorId", "Field", "HeadBoyId", "Number" },
                values: new object[] { 1, null, 1, null, 0 });

            migrationBuilder.InsertData(
                table: "Subjects",
                columns: new[] { "Id", "Name", "TypeSubject", "isExpired" },
                values: new object[,]
                {
                    { 1, "МДК 01.01", 0, false },
                    { 2, "МДК 01.03", 0, false },
                    { 3, "МДК 01.04", 0, false },
                    { 4, "МДК 01.05", 0, false },
                    { 5, "МДК 11.01", 0, false },
                    { 6, "МДК 01.01", 2, false },
                    { 7, "МДК 01.03", 2, false },
                    { 8, "МДК 01.04", 2, false },
                    { 9, "МДК 01.05", 2, false },
                    { 10, "МДК 11.01", 2, false },
                    { 11, "Физ-ра", 0, false },
                    { 12, "Иностранный язык", 0, false },
                    { 13, "Безопасность Жизндеятельности", 0, false },
                    { 14, "Экономика отросли", 0, false }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "IdChat", "Name", "Patronymic", "Role", "StudentGroupId", "Surname" },
                values: new object[,]
                {
                    { 1, 597239235, "Микаил", "Альбертович", "student", 1, "Османов" },
                    { 2, 597239236, "Алдан", "Маратович", "student", 1, "Омардибиров" },
                    { 3, 597239237, "Магомед", "Магомедов", "student", 1, "Аккаев" },
                    { 4, 597239237, "Султан", "Ахмедович", "curator", 1, "Шихмарданов" },
                    { 5, 597239237, "Саим", "Альбертович", "headboy", 1, "Саидов" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Shedule_FirstSubjectId",
                table: "Shedule",
                column: "FirstSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Shedule_FourthSubjectId",
                table: "Shedule",
                column: "FourthSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Shedule_SecondSubjectId",
                table: "Shedule",
                column: "SecondSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Shedule_ThirdSubjectId",
                table: "Shedule",
                column: "ThirdSubjectId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_CuratorId",
                table: "StudentGroups",
                column: "CuratorId");

            migrationBuilder.CreateIndex(
                name: "IX_StudentGroups_HeadBoyId",
                table: "StudentGroups",
                column: "HeadBoyId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_StudentGroupId",
                table: "Users",
                column: "StudentGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroups_Users_CuratorId",
                table: "StudentGroups",
                column: "CuratorId",
                principalTable: "Users",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StudentGroups_Users_HeadBoyId",
                table: "StudentGroups",
                column: "HeadBoyId",
                principalTable: "Users",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroups_Users_CuratorId",
                table: "StudentGroups");

            migrationBuilder.DropForeignKey(
                name: "FK_StudentGroups_Users_HeadBoyId",
                table: "StudentGroups");

            migrationBuilder.DropTable(
                name: "Shedule");

            migrationBuilder.DropTable(
                name: "Subjects");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "StudentGroups");
        }
    }
}

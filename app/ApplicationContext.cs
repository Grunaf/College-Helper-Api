using Microsoft.EntityFrameworkCore;
using app.Models;
using System.Xml;
using Microsoft.Extensions.Options;

namespace app
{

    public class ApplicationContext : DbContext
    {
        public DbSet<Student> Students { get; set; } = null!;
        public DbSet<Professor> Professors { get; set; } = null!;
        public DbSet<StudentGroup> StudentGroups { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<SheduleDay> SheduleDays { get; set; } = null!;
        public DbSet<StudentAbsence> StudentAbsence { get; set; } = null!;
        public DbSet<SheduleDaySubject> SheduleDaySubjects { get; set; } = null!;

        protected readonly IConfiguration _configuration;
        public string connString;

        public ApplicationContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var host = _configuration["DBHOST"] ?? "localhost";
            var port = _configuration.GetConnectionString("MYSQL_DBPORT");
            var password =_configuration.GetConnectionString("MYSQL_PASSWORD");
            var userid = _configuration.GetConnectionString("MYSQL_USER");
            var usersDataBase = _configuration.GetConnectionString("MYSQL_DATABASE");

            connString = $"server={host};port={port};userid={userid};password={password};database={usersDataBase}";
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 4, 0));

            options.UseMySql(connString, serverVersion, options =>
            {
                options.EnableRetryOnFailure(
                    maxRetryCount: 10, // Количество попыток
                    maxRetryDelay: TimeSpan.FromSeconds(30), // Максимальная задержка между попытками
                    errorNumbersToAdd: null); // Дополнительные коды ошибок, при которых следует повторить попытку
            });
            options.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGroup>()
                .Property(e => e.Field)
                .HasConversion<string>();


            modelBuilder.Entity<Student>()
                .HasIndex(u => u.ChatId)
                .IsUnique();

            modelBuilder.Entity<Student>()
                .HasIndex(s => s.StudentGroupId)
                .IsUnique(false);


            modelBuilder.Entity<SheduleDaySubject>()
            .HasKey(ss => new { ss.SheduleDayId, ss.SubjectId });

            modelBuilder.Entity<SheduleDaySubject>()
                .HasOne(ss => ss.SheduleDay)
                .WithMany(sd => sd.SheduleDaySubjects)
                .HasForeignKey(ss => ss.SheduleDayId);

            modelBuilder.Entity<SheduleDaySubject>()
                .HasOne(ss => ss.Subject)
                .WithMany(s => s.SheduleDaySubjects)
                .HasForeignKey(ss => ss.SubjectId)
                .IsRequired(false);

            modelBuilder.Entity<StudentGroup>().HasData(
                    new StudentGroup { Id = 1, Number = 20, Field = Field.ИСП },
                    new StudentGroup { Id = 2, Number = 21, Field = Field.ИСП }
            );

            modelBuilder.Entity<Student>().HasData(
                    new Student { Id = 1, IsHeadBoy = true, ChatId = 597239235, Surname = "Микаилов", Name = "Микаил", Patronymic = "Микаилович", StudentGroupId = 1},
                    new Student { Id = 2, IsHeadBoy = false, ChatId = 597239236, Surname = "Омардибиров", Name = "Алдан", Patronymic = "Алданович", StudentGroupId = 1},
                    new Student { Id = 3, IsHeadBoy = false, ChatId = 597239237, Surname = "Магомедов", Name = "Магомед", Patronymic = "Магомедович", StudentGroupId = 1},
                    new Student { Id = 4, IsHeadBoy = true, ChatId = 6418436193, Surname = "Деров", Name = "Дер", Patronymic = "Дерович", StudentGroupId = 2},
                    new Student { Id = 5, IsHeadBoy = false, ChatId = 597239249, Surname = "Керов", Name = "Кер", Patronymic = "Керович", StudentGroupId = 2},
                    new Student { Id = 6, IsHeadBoy = false, ChatId = 597239239, Surname = "Веров", Name = "Вер", Patronymic = "Верович", StudentGroupId = 2}
            );
            modelBuilder.Entity<StudentAbsence>().HasData(
                    new StudentAbsence { Id = 1, StudentId = 1, LessonNumber = 1 },
                    new StudentAbsence { Id = 2, StudentId = 2, LessonNumber = 2 },
                    new StudentAbsence { Id = 3, StudentId = 4, LessonNumber = 2 }
            );
            modelBuilder.Entity<Subject>().HasData(
                    new Subject { Id = 1, Title = "МДК 01.01"},
                    new Subject { Id = 2, Title = "МДК 01.03" },
                    new Subject { Id = 3, Title = "МДК 01.04" },
                    new Subject { Id = 4, Title = "МДК 01.05"},
                    new Subject { Id = 5, Title = "МДК 11.01" },
                    new Subject { Id = 6, Title = "МДК 01.01" },
                    new Subject { Id = 7, Title = "МДК 01.03" },
                    new Subject { Id = 8, Title = "МДК 01.04" },
                    new Subject { Id = 9, Title = "МДК 01.05" },
                    new Subject { Id = 10, Title = "МДК 11.01" },
                    new Subject { Id = 11, Title = "Физ-ра" },
                    new Subject { Id = 12, Title = "Иностранный язык" },
                    new Subject { Id = 13, Title = "Безопасность Жизндеятельности" },
                    new Subject { Id = 14, Title = "Экономика отросли" }
            );
        }
    }
}

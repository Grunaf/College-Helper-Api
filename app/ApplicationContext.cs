using Microsoft.EntityFrameworkCore;
using app.Models;
using System.Xml;

namespace app
{

    public class ApplicationContext : DbContext
    {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<StudentGroup> StudentGroups { get; set; } = null!;
        public DbSet<Subject> Subjects { get; set; } = null!;
        public DbSet<SheduleDay> Shedule { get; set; } = null!;
        public DbSet<StudentAttendance> StudentAttendances { get; set; } = null!;
        
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
            options.UseMySql(connString, ServerVersion.AutoDetect(connString));
            options.LogTo(Console.WriteLine).EnableSensitiveDataLogging();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StudentGroup>().HasData(
                    new StudentGroup { Id = 1, Number = 20, Field = Field.ИСП }
            );

            modelBuilder.Entity<User>()
                .Property(e => e.Role)
                .HasConversion<string>();

            modelBuilder.Entity<User>()
                .HasIndex(u => u.IdChat)
                .IsUnique();

            modelBuilder.Entity<StudentGroup>()
                .Property(e => e.Field)
                .HasConversion<string>();

            modelBuilder.Entity<User>().HasData(
                    new User { Id = 1, Role = Role.student, IdChat = 597239235, Surname = "Микаилов", Name = "Микаил", Patronymic = "Микаилович", StudentGroupId = 1},
                    new User { Id = 2, Role = Role.student, IdChat = 597239236, Surname = "Омардибиров", Name = "Алдан", Patronymic = "Алданович", StudentGroupId = 1},
                    new User { Id = 3, Role = Role.student, IdChat = 597239238, Surname = "Магомедов", Name = "Магомед", Patronymic = "Магомедович", StudentGroupId = 1},
                    new User { Id = 4, Role = Role.curator, IdChat = 597239239, Surname = "Деров", Name = "Дер", Patronymic = "Дерович", StudentGroupId = 1},
                    new User { Id = 5, Role = Role.headboy, IdChat = 597239240, Surname = "Керов", Name = "Кер", Patronymic = "Керович", StudentGroupId = 1}
            );
            modelBuilder.Entity<StudentAttendance>().HasData(
                    new StudentAttendance { Id = 1, StudentId = 1, NumberLesson = 1 },
                    new StudentAttendance { Id = 2, StudentId = 2, NumberLesson = 2 },
                    new StudentAttendance { Id = 3, StudentId = 3, NumberLesson = 2 }
            );
            modelBuilder.Entity<Subject>().HasData(
                    new Subject { Id = 1, Name = "МДК 01.01", TypeSubject = TypeSubject.Default},
                    new Subject { Id = 2, Name = "МДК 01.03", TypeSubject = TypeSubject.Default },
                    new Subject { Id = 3, Name = "МДК 01.04", TypeSubject = TypeSubject.Default},
                    new Subject { Id = 4, Name = "МДК 01.05", TypeSubject = TypeSubject.Default},
                    new Subject { Id = 5, Name = "МДК 11.01", TypeSubject = TypeSubject.Default },
                    new Subject { Id = 6, Name = "МДК 01.01", TypeSubject = TypeSubject.Lab },
                    new Subject { Id = 7, Name = "МДК 01.03", TypeSubject = TypeSubject.Lab },
                    new Subject { Id = 8, Name = "МДК 01.04", TypeSubject = TypeSubject.Lab },
                    new Subject { Id = 9, Name = "МДК 01.05", TypeSubject = TypeSubject.Lab },
                    new Subject { Id = 10, Name = "МДК 11.01", TypeSubject = TypeSubject.Lab },
                    new Subject { Id = 11, Name = "Физ-ра", TypeSubject = TypeSubject.Default },
                    new Subject { Id = 12, Name = "Иностранный язык", TypeSubject = TypeSubject.Default },
                    new Subject { Id = 13, Name = "Безопасность Жизндеятельности", TypeSubject = TypeSubject.Default },
                    new Subject { Id = 14, Name = "Экономика отросли", TypeSubject = TypeSubject.Default }
            );
            modelBuilder.Entity<StudentGroup>()
                .HasOne(sg => sg.Curator)
                .WithMany()
                .HasForeignKey(sg => sg.CuratorId);

            modelBuilder.Entity<StudentGroup>()
                .HasOne(sg => sg.HeadBoy)
                .WithMany()
                .HasForeignKey(sg => sg.HeadBoyId);
        }
    }
}

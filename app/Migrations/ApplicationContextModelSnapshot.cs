﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using app;

#nullable disable

namespace app.Migrations
{
    [DbContext(typeof(ApplicationContext))]
    partial class ApplicationContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.4")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            MySqlModelBuilderExtensions.AutoIncrementColumns(modelBuilder);

            modelBuilder.Entity("app.Models.Homework", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Comment")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<DateTime>("CreatedTime")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("StudentGroupId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("StudentGroupId");

                    b.HasIndex("SubjectId");

                    b.ToTable("Homeworks", (string)null);
                });

            modelBuilder.Entity("app.Models.HomeworkFile", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("FileId")
                        .IsRequired()
                        .HasMaxLength(255)
                        .HasColumnType("varchar(255)");

                    b.Property<int>("HomeworkId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("FileId")
                        .IsUnique();

                    b.HasIndex("HomeworkId");

                    b.ToTable("HomeworkFiles", (string)null);
                });

            modelBuilder.Entity("app.Models.Professor", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("IdChat")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsCurator")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Patronymic")
                        .HasColumnType("longtext");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Professors", (string)null);
                });

            modelBuilder.Entity("app.Models.SheduleDay", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<byte>("CountDay")
                        .HasColumnType("tinyint unsigned");

                    b.Property<byte>("CountWeek")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("StudentGroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentGroupId");

                    b.ToTable("SheduleDays", (string)null);
                });

            modelBuilder.Entity("app.Models.SheduleDaySubject", b =>
                {
                    b.Property<int>("SheduleDayId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<byte>("Spot")
                        .HasColumnType("tinyint unsigned");

                    b.Property<string>("Subgroup")
                        .HasColumnType("varchar(1)");

                    b.HasKey("SheduleDayId", "SubjectId", "Spot");

                    b.HasIndex("SubjectId");

                    b.ToTable("SheduleDaySubjects", (string)null);
                });

            modelBuilder.Entity("app.Models.Student", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<long>("ChatId")
                        .HasColumnType("bigint");

                    b.Property<bool>("IsHeadBoy")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .HasColumnType("longtext");

                    b.Property<string>("Patronymic")
                        .HasColumnType("longtext");

                    b.Property<int>("StudentGroupId")
                        .HasColumnType("int");

                    b.Property<string>("Surname")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("ChatId")
                        .IsUnique();

                    b.HasIndex("StudentGroupId");

                    b.ToTable("Students", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ChatId = 597239235L,
                            IsHeadBoy = true,
                            Name = "Микаил",
                            Patronymic = "Микаилович",
                            StudentGroupId = 1,
                            Surname = "Микаилов"
                        },
                        new
                        {
                            Id = 2,
                            ChatId = 597239236L,
                            IsHeadBoy = false,
                            Name = "Алдан",
                            Patronymic = "Алданович",
                            StudentGroupId = 1,
                            Surname = "Омардибиров"
                        },
                        new
                        {
                            Id = 3,
                            ChatId = 597239237L,
                            IsHeadBoy = false,
                            Name = "Магомед",
                            Patronymic = "Магомедович",
                            StudentGroupId = 1,
                            Surname = "Магомедов"
                        },
                        new
                        {
                            Id = 4,
                            ChatId = 6418436193L,
                            IsHeadBoy = true,
                            Name = "Дер",
                            Patronymic = "Дерович",
                            StudentGroupId = 2,
                            Surname = "Деров"
                        },
                        new
                        {
                            Id = 5,
                            ChatId = 597239249L,
                            IsHeadBoy = false,
                            Name = "Кер",
                            Patronymic = "Керович",
                            StudentGroupId = 2,
                            Surname = "Керов"
                        },
                        new
                        {
                            Id = 6,
                            ChatId = 597239239L,
                            IsHeadBoy = false,
                            Name = "Вер",
                            Patronymic = "Верович",
                            StudentGroupId = 2,
                            Surname = "Веров"
                        });
                });

            modelBuilder.Entity("app.Models.StudentAbsence", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("Date")
                        .HasColumnType("datetime(6)");

                    b.Property<byte>("LessonNumber")
                        .HasColumnType("tinyint unsigned");

                    b.Property<int>("StudentId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("StudentId");

                    b.ToTable("StudentAbsence", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Date = new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Local),
                            LessonNumber = (byte)1,
                            StudentId = 1
                        },
                        new
                        {
                            Id = 2,
                            Date = new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Local),
                            LessonNumber = (byte)2,
                            StudentId = 2
                        },
                        new
                        {
                            Id = 3,
                            Date = new DateTime(2024, 6, 17, 0, 0, 0, 0, DateTimeKind.Local),
                            LessonNumber = (byte)2,
                            StudentId = 4
                        });
                });

            modelBuilder.Entity("app.Models.StudentGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<int?>("CuratorId")
                        .HasColumnType("int");

                    b.Property<string>("Field")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Number")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("CuratorId");

                    b.ToTable("StudentGroups", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Field = "ИСП",
                            Number = 20
                        },
                        new
                        {
                            Id = 2,
                            Field = "ИСП",
                            Number = 21
                        });
                });

            modelBuilder.Entity("app.Models.StudentGroupSubject", b =>
                {
                    b.Property<int>("StudentGroupId")
                        .HasColumnType("int");

                    b.Property<int>("SubjectId")
                        .HasColumnType("int");

                    b.Property<bool>("IsExpired")
                        .HasColumnType("tinyint(1)");

                    b.HasKey("StudentGroupId", "SubjectId");

                    b.HasIndex("SubjectId");

                    b.ToTable("StudentGroupSubjects", (string)null);
                });

            modelBuilder.Entity("app.Models.Subject", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    MySqlPropertyBuilderExtensions.UseMySqlIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Subjects", (string)null);

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Title = "МДК 01.01"
                        },
                        new
                        {
                            Id = 2,
                            Title = "МДК 01.03"
                        },
                        new
                        {
                            Id = 3,
                            Title = "МДК 01.04"
                        },
                        new
                        {
                            Id = 4,
                            Title = "МДК 01.05"
                        },
                        new
                        {
                            Id = 5,
                            Title = "МДК 11.01"
                        },
                        new
                        {
                            Id = 6,
                            Title = "МДК 01.01"
                        },
                        new
                        {
                            Id = 7,
                            Title = "МДК 01.03"
                        },
                        new
                        {
                            Id = 8,
                            Title = "МДК 01.04"
                        },
                        new
                        {
                            Id = 9,
                            Title = "МДК 01.05"
                        },
                        new
                        {
                            Id = 10,
                            Title = "МДК 11.01"
                        },
                        new
                        {
                            Id = 11,
                            Title = "Физ-ра"
                        },
                        new
                        {
                            Id = 12,
                            Title = "Иностранный язык"
                        },
                        new
                        {
                            Id = 13,
                            Title = "Безопасность Жизндеятельности"
                        },
                        new
                        {
                            Id = 14,
                            Title = "Экономика отросли"
                        });
                });

            modelBuilder.Entity("app.Models.Homework", b =>
                {
                    b.HasOne("app.Models.StudentGroup", "StudentGroup")
                        .WithMany("Homeworks")
                        .HasForeignKey("StudentGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Models.Subject", "Subject")
                        .WithMany("Homeworks")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentGroup");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("app.Models.HomeworkFile", b =>
                {
                    b.HasOne("app.Models.Homework", "Homework")
                        .WithMany("HomeworkFiles")
                        .HasForeignKey("HomeworkId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Homework");
                });

            modelBuilder.Entity("app.Models.SheduleDay", b =>
                {
                    b.HasOne("app.Models.StudentGroup", "StudentGroup")
                        .WithMany()
                        .HasForeignKey("StudentGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentGroup");
                });

            modelBuilder.Entity("app.Models.SheduleDaySubject", b =>
                {
                    b.HasOne("app.Models.SheduleDay", "SheduleDay")
                        .WithMany("SheduleDaySubjects")
                        .HasForeignKey("SheduleDayId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Models.Subject", "Subject")
                        .WithMany("SheduleDaySubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("SheduleDay");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("app.Models.Student", b =>
                {
                    b.HasOne("app.Models.StudentGroup", "StudentGroup")
                        .WithOne("HeadBoy")
                        .HasForeignKey("app.Models.Student", "StudentGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentGroup");
                });

            modelBuilder.Entity("app.Models.StudentAbsence", b =>
                {
                    b.HasOne("app.Models.Student", "Student")
                        .WithMany("StudentAbsence")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Student");
                });

            modelBuilder.Entity("app.Models.StudentGroup", b =>
                {
                    b.HasOne("app.Models.Professor", "Curator")
                        .WithMany("StudentGroups")
                        .HasForeignKey("CuratorId");

                    b.Navigation("Curator");
                });

            modelBuilder.Entity("app.Models.StudentGroupSubject", b =>
                {
                    b.HasOne("app.Models.StudentGroup", "StudentGroup")
                        .WithMany("StudentGroupSubjects")
                        .HasForeignKey("StudentGroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("app.Models.Subject", "Subject")
                        .WithMany("StudentGroupSubjects")
                        .HasForeignKey("SubjectId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("StudentGroup");

                    b.Navigation("Subject");
                });

            modelBuilder.Entity("app.Models.Homework", b =>
                {
                    b.Navigation("HomeworkFiles");
                });

            modelBuilder.Entity("app.Models.Professor", b =>
                {
                    b.Navigation("StudentGroups");
                });

            modelBuilder.Entity("app.Models.SheduleDay", b =>
                {
                    b.Navigation("SheduleDaySubjects");
                });

            modelBuilder.Entity("app.Models.Student", b =>
                {
                    b.Navigation("StudentAbsence");
                });

            modelBuilder.Entity("app.Models.StudentGroup", b =>
                {
                    b.Navigation("HeadBoy")
                        .IsRequired();

                    b.Navigation("Homeworks");

                    b.Navigation("StudentGroupSubjects");
                });

            modelBuilder.Entity("app.Models.Subject", b =>
                {
                    b.Navigation("Homeworks");

                    b.Navigation("SheduleDaySubjects");

                    b.Navigation("StudentGroupSubjects");
                });
#pragma warning restore 612, 618
        }
    }
}

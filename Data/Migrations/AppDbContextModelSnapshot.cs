﻿// <auto-generated />
using System;
using AZLearn.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AZLearn.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("AZLearn.Models.Cohort", b =>
                {
                    b.Property<int>("CohortId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)");

                    b.Property<bool>("Archive")
                        .HasColumnType("boolean");

                    b.Property<int?>("Capacity")
                        .HasColumnType("int(3)");

                    b.Property<string>("City")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<string>("ModeOfTeaching")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("CohortId");

                    b.ToTable("Cohort");
                });

            modelBuilder.Entity("AZLearn.Models.CohortCourse", b =>
                {
                    b.Property<int>("CohortId")
                        .HasColumnType("int(10)");

                    b.Property<int>("CourseId")
                        .HasColumnType("int(10)");

                    b.Property<bool>("Archive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("date");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int(10)");

                    b.Property<string>("ResourcesLink")
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("date");

                    b.HasKey("CohortId", "CourseId");

                    b.HasIndex("CourseId");

                    b.HasIndex("InstructorId")
                        .HasName("FK_CohortCourse_Instructor");

                    b.ToTable("CohortCourse");
                });

            modelBuilder.Entity("AZLearn.Models.Course", b =>
                {
                    b.Property<int>("CourseId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)");

                    b.Property<bool>("Archive")
                        .HasColumnType("boolean");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<float>("DurationHrs")
                        .HasColumnType("float(5,2)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("CourseId");

                    b.ToTable("Course");
                });

            modelBuilder.Entity("AZLearn.Models.Grade", b =>
                {
                    b.Property<int>("RubricId")
                        .HasColumnType("int(10)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int(10)");

                    b.Property<bool>("Archive")
                        .HasColumnType("boolean");

                    b.Property<string>("InstructorComment")
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<int>("Mark")
                        .HasColumnType("int(3)");

                    b.Property<string>("StudentComment")
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("RubricId", "StudentId");

                    b.HasIndex("StudentId");

                    b.ToTable("Grade");
                });

            modelBuilder.Entity("AZLearn.Models.Homework", b =>
                {
                    b.Property<int>("HomeworkId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)");

                    b.Property<bool>("Archive")
                        .HasColumnType("boolean");

                    b.Property<float>("AvgCompletionTime")
                        .HasColumnType("float(5,2)");

                    b.Property<int>("CohortId")
                        .HasColumnType("int(10)");

                    b.Property<int>("CourseId")
                        .HasColumnType("int(10)");

                    b.Property<string>("DocumentLink")
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<DateTime?>("DueDate")
                        .HasColumnType("datetime");

                    b.Property<string>("GitHubClassRoomLink")
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<int>("InstructorId")
                        .HasColumnType("int(10)");

                    b.Property<bool>("IsAssignment")
                        .HasColumnType("boolean");

                    b.Property<DateTime?>("ReleaseDate")
                        .HasColumnType("datetime");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("varchar(100)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("HomeworkId");

                    b.HasIndex("CohortId")
                        .HasName("FK_Homework_Cohort");

                    b.HasIndex("CourseId")
                        .HasName("FK_Homework_Course");

                    b.HasIndex("InstructorId")
                        .HasName("FK_Homework_Instructor");

                    b.ToTable("Homework");
                });

            modelBuilder.Entity("AZLearn.Models.Notification", b =>
                {
                    b.Property<int>("NotificationId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)");

                    b.Property<bool>("Archive")
                        .HasColumnType("boolean");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<int>("StudentId")
                        .HasColumnType("int(10)");

                    b.HasKey("NotificationId");

                    b.HasIndex("StudentId")
                        .HasName("FK_Notification_Student");

                    b.ToTable("Notification");
                });

            modelBuilder.Entity("AZLearn.Models.Rubric", b =>
                {
                    b.Property<int>("RubricId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)");

                    b.Property<bool>("Archive")
                        .HasColumnType("boolean");

                    b.Property<string>("Criteria")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<int>("HomeworkId")
                        .HasColumnType("int(10)");

                    b.Property<bool>("IsChallenge")
                        .HasColumnType("boolean");

                    b.Property<int>("Weight")
                        .HasColumnType("int(3)");

                    b.HasKey("RubricId");

                    b.HasIndex("HomeworkId")
                        .HasName("FK_Rubric_Homework");

                    b.ToTable("Rubric");
                });

            modelBuilder.Entity("AZLearn.Models.ShoutOut", b =>
                {
                    b.Property<int>("ShoutOutId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)");

                    b.Property<bool>("Archive")
                        .HasColumnType("boolean");

                    b.Property<string>("Comment")
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<DateTime>("Date")
                        .HasColumnType("date");

                    b.Property<float>("DurationMins")
                        .HasColumnType("float(5,2)");

                    b.Property<int>("HomeworkId")
                        .HasColumnType("int(10)");

                    b.Property<int>("PeerId")
                        .HasColumnType("int(10)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int(10)");

                    b.Property<string>("Topic")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("ShoutOutId");

                    b.HasIndex("HomeworkId")
                        .HasName("FK_ShoutOut_Homework");

                    b.HasIndex("PeerId")
                        .HasName("FK_ShoutOut_Peer");

                    b.HasIndex("StudentId")
                        .HasName("FK_ShoutOut_Student");

                    b.ToTable("ShoutOut");
                });

            modelBuilder.Entity("AZLearn.Models.Timesheet", b =>
                {
                    b.Property<int>("TimesheetId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)");

                    b.Property<bool>("Archive")
                        .HasColumnType("boolean");

                    b.Property<int>("HomeworkId")
                        .HasColumnType("int(10)");

                    b.Property<float>("SolvingTime")
                        .HasColumnType("float(5,2)");

                    b.Property<int>("StudentId")
                        .HasColumnType("int(10)");

                    b.Property<float>("StudyTime")
                        .HasColumnType("float(5,2)");

                    b.HasKey("TimesheetId");

                    b.HasIndex("HomeworkId")
                        .HasName("FK_Timesheet_Homework");

                    b.HasIndex("StudentId")
                        .HasName("FK_Timesheet_Student");

                    b.ToTable("Timesheet");
                });

            modelBuilder.Entity("AZLearn.Models.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int(10)");

                    b.Property<bool>("Archive")
                        .HasColumnType("boolean");

                    b.Property<int?>("CohortId")
                        .HasColumnType("int(10)");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<bool>("IsInstructor")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("varchar(50)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.Property<string>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varchar(250)")
                        .HasAnnotation("MySql:CharSet", "utf8mb4")
                        .HasAnnotation("MySql:Collation", "utf8mb4_general_ci");

                    b.HasKey("UserId");

                    b.HasIndex("CohortId")
                        .HasName("FK_User_Cohort");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AZLearn.Models.CohortCourse", b =>
                {
                    b.HasOne("AZLearn.Models.Cohort", "Cohort")
                        .WithMany("CohortCourses")
                        .HasForeignKey("CohortId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AZLearn.Models.Course", "Course")
                        .WithMany("CohortCourses")
                        .HasForeignKey("CourseId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AZLearn.Models.User", "Instructor")
                        .WithMany("CohortCourses")
                        .HasForeignKey("InstructorId")
                        .HasConstraintName("FK_CohortCourse_Instructor")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AZLearn.Models.Grade", b =>
                {
                    b.HasOne("AZLearn.Models.Rubric", "Rubric")
                        .WithMany("Grades")
                        .HasForeignKey("RubricId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("AZLearn.Models.User", "Student")
                        .WithMany("Grades")
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("AZLearn.Models.Homework", b =>
                {
                    b.HasOne("AZLearn.Models.Cohort", "Cohort")
                        .WithMany("Homeworks")
                        .HasForeignKey("CohortId")
                        .HasConstraintName("FK_Homework_Cohort")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AZLearn.Models.Course", "Course")
                        .WithMany("Homeworks")
                        .HasForeignKey("CourseId")
                        .HasConstraintName("FK_Homework_Course")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AZLearn.Models.User", "Instructor")
                        .WithMany("Homeworks")
                        .HasForeignKey("InstructorId")
                        .HasConstraintName("FK_Homework_Instructor")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AZLearn.Models.Notification", b =>
                {
                    b.HasOne("AZLearn.Models.User", "Student")
                        .WithMany("Notifications")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_Notification_Student")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AZLearn.Models.Rubric", b =>
                {
                    b.HasOne("AZLearn.Models.Homework", "Homework")
                        .WithMany("Rubrics")
                        .HasForeignKey("HomeworkId")
                        .HasConstraintName("FK_Rubric_Homework")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AZLearn.Models.ShoutOut", b =>
                {
                    b.HasOne("AZLearn.Models.Homework", "Homework")
                        .WithMany("ShoutOuts")
                        .HasForeignKey("HomeworkId")
                        .HasConstraintName("FK_ShoutOut_Homework")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AZLearn.Models.User", "Peer")
                        .WithMany("ShoutOutsPeer")
                        .HasForeignKey("PeerId")
                        .HasConstraintName("FK_ShoutOut_Peer")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AZLearn.Models.User", "Student")
                        .WithMany("ShoutOutsStudent")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_ShoutOut_Student")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AZLearn.Models.Timesheet", b =>
                {
                    b.HasOne("AZLearn.Models.Homework", "Homework")
                        .WithMany("Timesheets")
                        .HasForeignKey("HomeworkId")
                        .HasConstraintName("FK_Timesheet_Homework")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.HasOne("AZLearn.Models.User", "Student")
                        .WithMany("Timesheets")
                        .HasForeignKey("StudentId")
                        .HasConstraintName("FK_Timesheet_Student")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();
                });

            modelBuilder.Entity("AZLearn.Models.User", b =>
                {
                    b.HasOne("AZLearn.Models.Cohort", "Cohort")
                        .WithMany("Users")
                        .HasForeignKey("CohortId")
                        .HasConstraintName("FK_User_Cohort")
                        .OnDelete(DeleteBehavior.Restrict);
                });
#pragma warning restore 612, 618
        }
    }
}

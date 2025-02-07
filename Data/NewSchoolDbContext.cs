using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using NewSchoolDB.Models;

namespace NewSchoolDB.Data;

public partial class NewSchoolDbContext : DbContext
{
    public NewSchoolDbContext()
    {
    }

    public NewSchoolDbContext(DbContextOptions<NewSchoolDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Grade> Grades { get; set; }

    public virtual DbSet<Profession> Professions { get; set; }

    public virtual DbSet<Scale> Scales { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Subject> Subjects { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost;Database=NewSchoolDB;Integrated Security=True;Trust Server Certificate=true;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Classes__3214EC2746006ABF");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClassName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Departme__3214EC273D7A4013");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartmentName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Employee__3214EC274D025FAA");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DepartmentId).HasColumnName("Department_ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.ProfessionsId).HasColumnName("Professions_ID");
            entity.Property(e => e.Salary).HasColumnType("decimal(18, 0)");

            entity.HasOne(d => d.Department).WithMany(p => p.Employees)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("FK__Employees__Depar__3B75D760");

            entity.HasOne(d => d.Professions).WithMany(p => p.Employees)
                .HasForeignKey(d => d.ProfessionsId)
                .HasConstraintName("FK__Employees__Profe__3C69FB99");
        });

        modelBuilder.Entity<Grade>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Grades__3214EC272D8C9B5C");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.EmployeesId).HasColumnName("Employees_ID");
            entity.Property(e => e.ScalesId).HasColumnName("Scales_ID");
            entity.Property(e => e.StudentsId).HasColumnName("Students_ID");
            entity.Property(e => e.SubjectsId).HasColumnName("Subjects_ID");

            entity.HasOne(d => d.Employees).WithMany(p => p.Grades)
                .HasForeignKey(d => d.EmployeesId)
                .HasConstraintName("FK__Grades__Employee__49C3F6B7");

            entity.HasOne(d => d.Scales).WithMany(p => p.Grades)
                .HasForeignKey(d => d.ScalesId)
                .HasConstraintName("FK__Grades__Scales_I__4AB81AF0");

            entity.HasOne(d => d.Students).WithMany(p => p.Grades)
                .HasForeignKey(d => d.StudentsId)
                .HasConstraintName("FK__Grades__Students__47DBAE45");

            entity.HasOne(d => d.Subjects).WithMany(p => p.Grades)
                .HasForeignKey(d => d.SubjectsId)
                .HasConstraintName("FK__Grades__Subjects__48CFD27E");
        });

        modelBuilder.Entity<Profession>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Professi__3214EC2715BFB51B");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Title)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Scale>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Scales__3214EC27198E72FC");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Mark)
                .HasMaxLength(5)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Students__3214EC2705B7C3C7");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.ClassesId).HasColumnName("Classes_ID");
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Ssn)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("SSN");

            entity.HasOne(d => d.Classes).WithMany(p => p.Students)
                .HasForeignKey(d => d.ClassesId)
                .HasConstraintName("FK__Students__Classe__412EB0B6");
        });

        modelBuilder.Entity<Subject>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Subjects__3214EC27A30A53A3");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.SubjectName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

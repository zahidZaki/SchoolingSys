using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Teacherinfo.DLL.Models;

namespace Teacherinfo.DLL.Db_Context;

public partial class TeachterDbContext : DbContext
{
    public TeachterDbContext()
    {
    }

    public TeachterDbContext(DbContextOptions<TeachterDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TeacherTable> TeacherTables { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server =ZAHID-GUL\\SQLEXPRESS;Database=Teacher_DB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TeacherTable>(entity =>
        {
            entity.HasKey(e => e.TeacherId).HasName("PK_Teacher");

            entity.ToTable("TeacherTable");

            entity.Property(e => e.TeacherId).HasColumnName("Teacher_Id");
            entity.Property(e => e.TeacherEmail)
                .HasMaxLength(100)
                .HasColumnName("Teacher_Email");
            entity.Property(e => e.TeacherHireDate)
                .HasColumnType("datetime")
                .HasColumnName("Teacher_Hire_Date");
            entity.Property(e => e.TeacherIsActive).HasColumnName("Teacher_Is_Active");
            entity.Property(e => e.TeacherName)
                .HasMaxLength(100)
                .HasColumnName("Teacher_Name");
            entity.Property(e => e.TeacherPhone)
                .HasMaxLength(100)
                .HasColumnName("Teacher_Phone");
            entity.Property(e => e.TeacherQualification)
                .HasMaxLength(100)
                .HasColumnName("Teacher_Qualification");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

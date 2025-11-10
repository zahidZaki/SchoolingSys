using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StudentInfo.DLL.Models;

namespace StudentInfo.DLL.Db_Context;

public partial class StudentDbContext : DbContext
{
    public StudentDbContext()
    {
    }

    public StudentDbContext(DbContextOptions<StudentDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Student> Students { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server =ZAHID-GUL\\SQLEXPRESS;Database=Student_DB;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.StdId);

            entity.ToTable("Student");

            entity.Property(e => e.StdId).HasColumnName("std_id");
            entity.Property(e => e.ClsId).HasColumnName("cls_id");
            entity.Property(e => e.StdDateOfBirth)
                .HasColumnType("datetime")
                .HasColumnName("std_dateOfBirth");
            entity.Property(e => e.StdEmail)
                .HasMaxLength(50)
                .HasColumnName("std_email");
            entity.Property(e => e.StdFirstName)
                .HasMaxLength(50)
                .HasColumnName("std_firstName");
            entity.Property(e => e.StdGender)
                .HasMaxLength(10)
                .HasColumnName("std_gender");
            entity.Property(e => e.StdLastName)
                .HasMaxLength(50)
                .HasColumnName("std_lastName");
            entity.Property(e => e.StdRollNumber)
                .HasMaxLength(25)
                .HasColumnName("std_rollNumber");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

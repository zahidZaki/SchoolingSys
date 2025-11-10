using System;
using System.Collections.Generic;
using ClassInfo.DLL.Models;
using Microsoft.EntityFrameworkCore;

namespace ClassInfo.DLL.Db_Context;

public partial class ClassesDbContext : DbContext
{
    public ClassesDbContext()
    {
    }

    public ClassesDbContext(DbContextOptions<ClassesDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Class> Classes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server =ZAHID-GUL\\SQLEXPRESS;Database=Class_Db;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Class>(entity =>
        {
            entity.HasNoKey();

            entity.Property(e => e.ClsId).HasColumnName("cls_Id");
            entity.Property(e => e.ClsName)
                .HasMaxLength(50)
                .HasColumnName("cls_Name");
            entity.Property(e => e.ClsRoomNo)
                .HasMaxLength(20)
                .HasColumnName("cls_RoomNo");
            entity.Property(e => e.ClsSection)
                .HasMaxLength(20)
                .HasColumnName("cls_Section");
            entity.Property(e => e.ClsTeacherId)
                .HasMaxLength(20)
                .HasColumnName("cls_TeacherId");
            entity.Property(e => e.Id).HasColumnName("id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

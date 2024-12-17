using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace API_Web.Models;

public partial class BaseProjectContext : DbContext
{
    public BaseProjectContext()
    {
    }

    public BaseProjectContext(DbContextOptions<BaseProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<UserList> UserLists { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=container_base;user=root;password=root;database=base_project", ServerVersion.Parse("8.0.39-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<UserList>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PRIMARY");

            entity.ToTable("user_list");

            entity.Property(e => e.UserId)
                .ValueGeneratedNever()
                .HasColumnName("UserID");
            entity.Property(e => e.Login).HasMaxLength(45);
            entity.Property(e => e.Name).HasMaxLength(45);
            entity.Property(e => e.Password).HasMaxLength(45);
            entity.Property(e => e.Patronymic).HasMaxLength(45);
            entity.Property(e => e.Surname).HasMaxLength(45);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

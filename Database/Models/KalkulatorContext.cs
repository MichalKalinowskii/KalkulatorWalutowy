using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace Database.Models;

public partial class KalkulatorContext : DbContext
{
    public KalkulatorContext()
    {
    }

    public KalkulatorContext(DbContextOptions<KalkulatorContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Nbp> Nbps { get; set; }

    public virtual DbSet<Nbprate> Nbprates { get; set; }

    public virtual DbSet<User> Users { get; set; }

    public virtual DbSet<UsersNbp> UsersNbps { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseMySql("server=127.0.0.1;port=3306;database=kalkulator;user=root;password=secret", Microsoft.EntityFrameworkCore.ServerVersion.Parse("9.1.0-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_0900_ai_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Nbp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("NBP");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.No).HasMaxLength(50);
            entity.Property(e => e.TableType).HasMaxLength(10);
        });

        modelBuilder.Entity<Nbprate>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("NBPRates");

            entity.HasIndex(e => e.Nbpid, "NBPID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Code).HasMaxLength(10);
            entity.Property(e => e.Currency).HasMaxLength(50);
            entity.Property(e => e.Nbpid).HasColumnName("NBPID");

            entity.HasOne(d => d.Nbp).WithMany(p => p.Nbprates)
                .HasForeignKey(d => d.Nbpid)
                .HasConstraintName("NBPRates_ibfk_1");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Password).HasMaxLength(50);
            entity.Property(e => e.Username).HasMaxLength(50);
        });

        modelBuilder.Entity<UsersNbp>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PRIMARY");

            entity.ToTable("UsersNBP");

            entity.HasIndex(e => e.Nbpid, "NBPID");

            entity.HasIndex(e => e.UserId, "UserID");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Nbpid).HasColumnName("NBPID");
            entity.Property(e => e.UserId).HasColumnName("UserID");

            entity.HasOne(d => d.Nbp).WithMany(p => p.UsersNbps)
                .HasForeignKey(d => d.Nbpid)
                .HasConstraintName("UsersNBP_ibfk_2");

            entity.HasOne(d => d.User).WithMany(p => p.UsersNbps)
                .HasForeignKey(d => d.UserId)
                .HasConstraintName("UsersNBP_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

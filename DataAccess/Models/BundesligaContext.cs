using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Models;

public partial class BundesligaContext : DbContext
{
    public BundesligaContext()
    {
    }

    public BundesligaContext(DbContextOptions<BundesligaContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Liga> Ligas { get; set; }

    public virtual DbSet<Spiel> Spiels { get; set; }

    public virtual DbSet<Spieler> Spielers { get; set; }

    public virtual DbSet<Verein> Vereins { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
    {
        optionsBuilder.UseSqlServer("Server=DESKTOP-E2NGEBR;Database=bundesliga;Trusted_Connection=True;TrustServerCertificate=True;");
    
    }
    //https://stackoverflow.com/questions/75439434/net-core-6-to-protect-potentially-sensitive-information-in-your-connection-st
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Liga>(entity =>
        {
            entity.HasKey(e => e.LigaNr).HasName("pk_liga");

            entity.ToTable("Liga");

            entity.HasIndex(e => e.Meister, "idx_meister");

            entity.Property(e => e.LigaNr)
                .ValueGeneratedNever()
                .HasColumnName("Liga_Nr");
            entity.Property(e => e.Erstaustragung).HasColumnType("date");
            entity.Property(e => e.Rekordspieler)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SpieleRekordspieler).HasColumnName("Spiele_Rekordspieler");
            entity.Property(e => e.Verband)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.MeisterNavigation).WithMany(p => p.Ligas)
                .HasForeignKey(d => d.Meister)
                .HasConstraintName("fk_liga_verein");
        });

        modelBuilder.Entity<Spiel>(entity =>
        {
            entity.HasKey(e => e.SpielId).HasName("pk_Spiel");

            entity.ToTable("Spiel");

            entity.HasIndex(e => e.Gast, "idx_Gast");

            entity.HasIndex(e => e.Heim, "idx_Heim");

            entity.Property(e => e.SpielId).HasColumnName("Spiel_ID");
            entity.Property(e => e.Datum).HasColumnType("date");
            entity.Property(e => e.ToreGast).HasColumnName("Tore_Gast");
            entity.Property(e => e.ToreHeim).HasColumnName("Tore_Heim");

            entity.HasOne(d => d.GastNavigation).WithMany(p => p.SpielGastNavigations)
                .HasForeignKey(d => d.Gast)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Spiel_verein_gast");

            entity.HasOne(d => d.HeimNavigation).WithMany(p => p.SpielHeimNavigations)
                .HasForeignKey(d => d.Heim)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Spiel_verein_heim");
        });

        modelBuilder.Entity<Spieler>(entity =>
        {
            entity.HasKey(e => e.SpielerId).HasName("pk_Spieler");

            entity.ToTable("Spieler");

            entity.HasIndex(e => e.VereinsId, "idx_Vereins_id");

            entity.Property(e => e.SpielerId).HasColumnName("Spieler_ID");
            entity.Property(e => e.Land)
                .HasMaxLength(255)
                .IsUnicode(false);
            entity.Property(e => e.SpielerName)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("Spieler_Name");
            entity.Property(e => e.TrikotNr).HasColumnName("Trikot_Nr");
            entity.Property(e => e.VereinsId).HasColumnName("Vereins_ID");

            entity.HasOne(d => d.Vereins).WithMany(p => p.Spielers)
                .HasForeignKey(d => d.VereinsId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_Spieler_verein");
        });

        modelBuilder.Entity<Verein>(entity =>
        {
            entity.HasKey(e => e.VId).HasName("pk_Verein");

            entity.ToTable("Verein");

            entity.HasIndex(e => e.Liga, "idx_liga");

            entity.HasIndex(e => e.Name, "unq_name").IsUnique();

            entity.Property(e => e.VId)
                .ValueGeneratedNever()
                .HasColumnName("V_ID");
            entity.Property(e => e.Name)
                .IsRequired()
                .HasMaxLength(255)
                .IsUnicode(false);

            entity.HasOne(d => d.LigaNavigation).WithMany(p => p.Vereins)
                .HasForeignKey(d => d.Liga)
                .HasConstraintName("fk_verein_liga");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

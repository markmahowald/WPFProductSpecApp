using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Pomelo.EntityFrameworkCore.MySql.Scaffolding.Internal;

namespace ProductSpecificationApp.Data.EntityFrameworkClasses;

public partial class ProductSpecificationDbContext : DbContext
{
    public ProductSpecificationDbContext()
    {
    }

    public ProductSpecificationDbContext(DbContextOptions<ProductSpecificationDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TblBranding> TblBrandings { get; set; }

    public virtual DbSet<TblClient> TblClients { get; set; }

    public virtual DbSet<TblMaterial> TblMaterials { get; set; }

    public virtual DbSet<TblMold> TblMolds { get; set; }

    public virtual DbSet<TblProduct> TblProducts { get; set; }

    public virtual DbSet<TblProductbranding> TblProductbrandings { get; set; }

    public virtual DbSet<TblProductmaterial> TblProductmaterials { get; set; }

    public virtual DbSet<TblProductmold> TblProductmolds { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySql("server=localhost;database=ProductSpecificationDB;user=root;password=root", ServerVersion.Parse("5.7.24-mysql"));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8_general_ci")
            .HasCharSet("utf8");

        modelBuilder.Entity<TblBranding>(entity =>
        {
            entity.HasKey(e => e.BrandingId).HasName("PRIMARY");

            entity.ToTable("tbl_branding");

            entity.HasIndex(e => e.ClientId, "ClientId");

            entity.Property(e => e.BrandingId).HasColumnType("int(11)");
            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Type).HasColumnType("enum('CardboardSleve','BrandedBag','Shrinkwrap')");
            entity.Property(e => e.UpdateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.TblBrandings)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("tbl_branding_ibfk_1");
        });

        modelBuilder.Entity<TblClient>(entity =>
        {
            entity.HasKey(e => e.ClientId).HasName("PRIMARY");

            entity.ToTable("tbl_clients");

            entity.Property(e => e.ClientId).HasColumnType("int(11)");
            entity.Property(e => e.ClientName).HasMaxLength(255);
            entity.Property(e => e.UpdateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TblMaterial>(entity =>
        {
            entity.HasKey(e => e.MaterialId).HasName("PRIMARY");

            entity.ToTable("tbl_materials");

            entity.Property(e => e.MaterialId).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Height).HasPrecision(10, 2);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Sku)
                .HasMaxLength(255)
                .HasColumnName("SKU");
            entity.Property(e => e.Unit).HasMaxLength(255);
            entity.Property(e => e.UpdateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Width).HasPrecision(10, 2);
        });

        modelBuilder.Entity<TblMold>(entity =>
        {
            entity.HasKey(e => e.MoldId).HasName("PRIMARY");

            entity.ToTable("tbl_molds");

            entity.Property(e => e.MoldId).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.UpdateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
        });

        modelBuilder.Entity<TblProduct>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("tbl_products");

            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.Description).HasColumnType("text");
            entity.Property(e => e.Height).HasPrecision(10, 2);
            entity.Property(e => e.Name).HasMaxLength(255);
            entity.Property(e => e.Sku)
                .HasMaxLength(255)
                .HasColumnName("SKU");
            entity.Property(e => e.Unit).HasMaxLength(255);
            entity.Property(e => e.UpdateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");
            entity.Property(e => e.Width).HasPrecision(10, 2);
        });

        modelBuilder.Entity<TblProductbranding>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.BrandingId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("tbl_productbranding");

            entity.HasIndex(e => e.BrandingId, "BrandingId");

            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.BrandingId).HasColumnType("int(11)");
            entity.Property(e => e.UpdateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Branding).WithMany(p => p.TblProductbrandings)
                .HasForeignKey(d => d.BrandingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_productbranding_ibfk_2");

            entity.HasOne(d => d.Product).WithMany(p => p.TblProductbrandings)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_productbranding_ibfk_1");
        });

        modelBuilder.Entity<TblProductmaterial>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.MaterialId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("tbl_productmaterials");

            entity.HasIndex(e => e.MaterialId, "MaterialId");

            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.MaterialId).HasColumnType("int(11)");
            entity.Property(e => e.UpdateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Material).WithMany(p => p.TblProductmaterials)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_productmaterials_ibfk_2");

            entity.HasOne(d => d.Product).WithMany(p => p.TblProductmaterials)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_productmaterials_ibfk_1");
        });

        modelBuilder.Entity<TblProductmold>(entity =>
        {
            entity.HasKey(e => new { e.ProductId, e.MoldId })
                .HasName("PRIMARY")
                .HasAnnotation("MySql:IndexPrefixLength", new[] { 0, 0 });

            entity.ToTable("tbl_productmolds");

            entity.HasIndex(e => e.MoldId, "MoldId");

            entity.Property(e => e.ProductId).HasColumnType("int(11)");
            entity.Property(e => e.MoldId).HasColumnType("int(11)");
            entity.Property(e => e.UpdateDate)
                .ValueGeneratedOnAddOrUpdate()
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Mold).WithMany(p => p.TblProductmolds)
                .HasForeignKey(d => d.MoldId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_productmolds_ibfk_2");

            entity.HasOne(d => d.Product).WithMany(p => p.TblProductmolds)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("tbl_productmolds_ibfk_1");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

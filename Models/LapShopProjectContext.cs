using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ecommerce.Models;

public partial class LapShopProjectContext : IdentityDbContext<ApplicationUser>
{
    public LapShopProjectContext()
    {
    }

    public LapShopProjectContext(DbContextOptions<LapShopProjectContext> options)
        : base(options)
    {
    }

    public virtual DbSet<TbCategory> TbCategories { get; set; }

    public virtual DbSet<TbImage> TbImages { get; set; }

    public virtual DbSet<TbLaptop> TbLaptops { get; set; }

    public virtual DbSet<TbSetting> TbSettings { get; set; }
    public virtual DbSet<TbPrices> TbPrices { get; set; }
    public virtual DbSet<CartModel> TbCarts { get; set; }
    public virtual DbSet<CartItemModel> TbCartProducts { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=DESKTOP-S1KCJF8\\SQLEXPRESS;Database=LapShopProject;Trusted_Connection=true;Encrypt=false;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<TbCategory>(entity =>
        {
            entity.HasKey(e => e.CategoryId);
        });

        modelBuilder.Entity<TbImage>(entity =>
        {
            entity.HasIndex(e => e.LaptopId, "IX_TbImages_LaptopId");

            entity.HasOne(d => d.Laptop).WithMany(p => p.TbImages).HasForeignKey(d => d.LaptopId);
        });

        modelBuilder.Entity<TbLaptop>(entity =>
        {
            entity.Property(e => e.Gpu).HasColumnName("GPU");
            entity.Property(e => e.Price).HasColumnType("decimal(18, 2)");
            entity.Property(e => e.ScreenSize).HasColumnType("decimal(18, 2)");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}

using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Context;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    // Veritabanı tablolarını temsil eden DbSet'ler
    public DbSet<Product> Products { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Decimal alanı için hassasiyet (precision) ve scale (ondalık basamak) ayarlanıyor
        modelBuilder.Entity<Product>(entity =>
        {
            entity.Property(e => e.Price).HasColumnType("decimal(18,2)"); // 18 toplam basamak, 2 ondalık basamak
        });

        // Örnek model yapılandırması
        modelBuilder.Entity<Product>().HasData(
            new Product { Id = 1, Name = "Sample Product", Description = "Sample Description", Price = 10.00m, Stock = 100, Category = "Sample Category" }
        );
    }
}

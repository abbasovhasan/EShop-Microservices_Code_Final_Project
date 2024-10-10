using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Persistance.Context;

public class OrderDbContext : DbContext
{
    public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options)
    {
    }

    // DbSet'ler (Veritabanı tabloları)
    public DbSet<Order> Orders { get; set; }
    public DbSet<OrderItem> OrderItems { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Payment> Payments { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Order entity yapılandırması
        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(o => o.Id);

            // One-to-Many: Order -> OrderItems
            entity.HasMany(o => o.OrderItems)
                  .WithOne(oi => oi.Order)
                  .HasForeignKey(oi => oi.OrderId);

            // One-to-One: Order -> Payment
            entity.HasOne(o => o.Payment)
                  .WithOne(p => p.Order)
                  .HasForeignKey<Payment>(p => p.OrderId);

            entity.Property(o => o.TotalAmount)
                  .HasColumnType("decimal(18,2)");

            // Enum dönüşümü (OrderStatus)
            entity.Property(o => o.Status)
                  .HasConversion<int>(); // Enum için int dönüşümü

            // Eğer seed data gerekiyorsa, buraya ekleyebilirsiniz
        });

        // OrderItem entity yapılandırması
        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(oi => oi.Id);
            entity.Property(oi => oi.UnitPrice)
                  .HasColumnType("decimal(18,2)"); // UnitPrice kullanımı düzeltildi
            entity.Property(oi => oi.Quantity).IsRequired();
        });

        // Payment entity yapılandırması
        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(p => p.Id);
            entity.Property(p => p.Amount)
                  .HasColumnType("decimal(18,2)");
        });

        // Customer entity yapılandırması
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(c => c.Id);

            // FirstName ve LastName için MaxLength kısıtlamaları
            entity.Property(c => c.FirstName)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(c => c.LastName)
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(c => c.Email)
                  .HasMaxLength(100)
                  .IsRequired();

            entity.Property(c => c.PhoneNumber)
                  .HasMaxLength(20)
                  .IsRequired();
        });
    }
}

using Domain.Enums;
using Domain.Models;
using Domain.RepositoriesAbstraction;
using Microsoft.EntityFrameworkCore;
using Persistance.Context;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.RepositoriesConcretes;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntities
{
    private readonly OrderDbContext _context;  // OrderDbContext'i kullanıyoruz

    public ReadRepository(OrderDbContext context)
    {
        _context = context;
    }

    // DbSet'e erişim
    public DbSet<T> Table => _context.Set<T>();

    // Tüm verileri çekme
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        return await Table.ToListAsync();
    }

    // Id'ye göre veri çekme
    public async Task<T> GetByIdAsync(int id)
    {
        return await Table.FirstOrDefaultAsync(e => e.Id == id);
    }

    // Siparişe ait tüm ürünleri çekme
    public async Task<IEnumerable<OrderItem>> GetOrderItemsAsync(int orderId)
    {
        var order = await _context.Orders.Include(o => o.OrderItems)
                                         .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order != null)
        {
            return order.OrderItems;
        }
        else
        {
            throw new KeyNotFoundException("Order not found");
        }
    }

    // Müşterinin tüm siparişlerini çekme
    public async Task<IEnumerable<Order>> GetOrdersByCustomerIdAsync(int customerId)
    {
        return await _context.Orders
                             .Where(o => o.CustomerId == customerId)
                             .ToListAsync();
    }

    // Belirli bir durumdaki tüm siparişleri çekme
    public async Task<IEnumerable<Order>> GetOrdersByStatusAsync(OrderStatus status)
    {
        return await _context.Orders
                             .Where(o => o.Status == status)
                             .ToListAsync();
    }
}

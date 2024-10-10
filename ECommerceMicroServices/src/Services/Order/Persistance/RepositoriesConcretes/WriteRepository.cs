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
public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntities
{
    private readonly OrderDbContext _context;

    public WriteRepository(OrderDbContext context) // OrderDbContext kullanıyoruz
    {
        _context = context;
    }

    // DbSet'e erişim
    public DbSet<T> Table => _context.Set<T>();

    // Yeni entity ekleme
    public async Task AddAsync(T entity)
    {
        await Table.AddAsync(entity);
        await _context.SaveChangesAsync();
    }

    // Siparişe yeni ürün ekleme
    public async Task AddOrderItemAsync(int orderId, OrderItem orderItem)
    {
        var order = await _context.Orders.Include(o => o.OrderItems)
                                         .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order != null)
        {
            order.OrderItems.Add(orderItem);
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Order not found");
        }
    }

    // Siparişten ürün silme
    public async Task RemoveOrderItemAsync(int orderId, int orderItemId)
    {
        var order = await _context.Orders.Include(o => o.OrderItems)
                                         .FirstOrDefaultAsync(o => o.Id == orderId);

        if (order != null)
        {
            var orderItem = order.OrderItems.FirstOrDefault(oi => oi.Id == orderItemId);

            if (orderItem != null)
            {
                order.OrderItems.Remove(orderItem);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new KeyNotFoundException("Order item not found");
            }
        }
        else
        {
            throw new KeyNotFoundException("Order not found");
        }
    }

    // Entity silme
    public async Task DeleteAsync(int id)
    {
        var entity = await Table.FindAsync(id);
        if (entity != null)
        {
            Table.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }

    // Sipariş durumu güncelleme
    public async Task UpdateOrderStatusAsync(int orderId, OrderStatus status)
    {
        var order = await _context.Orders.FirstOrDefaultAsync(o => o.Id == orderId);

        if (order != null)
        {
            order.Status = status;
            await _context.SaveChangesAsync();
        }
        else
        {
            throw new KeyNotFoundException("Order not found");
        }
    }

    // Entity güncelleme
    public async Task UpdateAsync(T entity)
    {
        Table.Update(entity);
        await _context.SaveChangesAsync();
    }
}
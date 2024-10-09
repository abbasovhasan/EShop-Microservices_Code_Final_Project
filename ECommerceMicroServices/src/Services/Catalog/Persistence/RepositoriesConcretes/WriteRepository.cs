using Domain.Models;
using Domain.RepositoriesAbstraction;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.RepositoriesConcretes;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntities
{
    private readonly AppDbContext _context;

    public WriteRepository(AppDbContext context)
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

    // Entity güncelleme
    public async Task UpdateAsync(T entity)
    {
        Table.Update(entity);
        await _context.SaveChangesAsync();
    }
}


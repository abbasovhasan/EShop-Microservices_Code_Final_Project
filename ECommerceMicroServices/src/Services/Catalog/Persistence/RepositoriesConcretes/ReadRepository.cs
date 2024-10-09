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

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntities
{
    private readonly AppDbContext _context;

    public ReadRepository(AppDbContext context)
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
}



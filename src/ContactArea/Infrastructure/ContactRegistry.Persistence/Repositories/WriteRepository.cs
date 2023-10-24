using ContactRegistry.Domain.Entities.Common;
using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace ContactRegistry.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly ContactDbContext _context;

    public WriteRepository(ContactDbContext context)
    {
        _context=context;
    }

    public DbSet<T> Table => _context.Set<T>();

    public async Task<bool> AddAsync(T model)
    {
        EntityEntry<T> entityEntry = await Table.AddAsync(model);
        return entityEntry.State == EntityState.Added;
    }

    public async Task<bool> AddRangeAsync(List<T> data)
    {
        await Table.AddRangeAsync(data);
        return true;
    }

    public bool Remove(T model)
    {
        EntityEntry<T> entityEntry = Table.Remove(model);
        return entityEntry.State == EntityState.Deleted;
    }

    public async Task<bool> RemoveAsync(string id)
    {
        T model = await Table.FindAsync(Guid.Parse(id));
        if (model is null)
            return false;
        return Remove(model);
    }

    public bool RemoveRange(List<T> data)
    {
        Table.RemoveRange(data);
        return true;
    }

    public bool Update(T model)
    {
        EntityEntry<T> entityEntry = Table.Update(model);
        return entityEntry.State == EntityState.Modified;
    }

    public async Task<int> SaveAsync()
        => await _context.SaveChangesAsync();
}
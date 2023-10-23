using ContactRegistry.Domain.Entities.Common;
using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application;
using Microsoft.EntityFrameworkCore;

namespace ContactRegistry.Persistence.Repositories;

public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntity
{
    private readonly ContactDbContext _context;

    public WriteRepository(ContactDbContext context)
    {
        _context=context;
    }

    public DbSet<T> Table => throw new NotImplementedException();

    public Task<bool> AddAsync(T model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> AddRangeAsync(List<T> datas)
    {
        throw new NotImplementedException();
    }

    public bool Remove(T model)
    {
        throw new NotImplementedException();
    }

    public Task<bool> RemoveAsync(string id)
    {
        throw new NotImplementedException();
    }

    public bool RemoveRange(List<T> data)
    {
        throw new NotImplementedException();
    }

    public Task<int> SaveAsync()
    {
        throw new NotImplementedException();
    }

    public bool Update(T model)
    {
        throw new NotImplementedException();
    }
}
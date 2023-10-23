using ContactRegistry.Domain.Entities.Common;
using ContactRegistry.Persistence.Contexts;
using ContantRegistry.Application;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ContactRegistry.Persistence.Repositories;

public class ReadRepository<T> : IReadRepository<T> where T : BaseEntity
{
    private readonly ContactDbContext _context;

    public ReadRepository(ContactDbContext context)
    {
        _context=context;
    }

    public DbSet<T> Table => throw new NotImplementedException();

    public IQueryable<T> GetAll(bool tracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetByIdAsync(string id, bool tracking = true)
    {
        throw new NotImplementedException();
    }

    public Task<T> GetSingleAsync(Expression<Func<T, bool>> method, bool tracking = true)
    {
        throw new NotImplementedException();
    }

    public IQueryable<T> GetWhere(Expression<Func<T, bool>> method, bool tracking = true)
    {
        throw new NotImplementedException();
    }
}
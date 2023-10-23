using ContactRegistry.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;

namespace ContantRegistry.Application
{
    public interface IRepository<T> where T : BaseEntity
    {
        DbSet<T> Table { get; }
    }
}
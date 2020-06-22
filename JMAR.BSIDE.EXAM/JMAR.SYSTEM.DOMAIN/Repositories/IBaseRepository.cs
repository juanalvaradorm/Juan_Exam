using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace JMAR.SYSTEM.DOMAIN.Repositories
{
    public interface IBaseRepository<T> : IDisposable where T : class
    {

        IQueryable<T> GetAllAsync(CancellationToken ct = default(CancellationToken));
        Task<T> GetByIdAsync(int? id, CancellationToken ct = default(CancellationToken));
        Task<T> AddAsync(T newEntity, CancellationToken ct = default(CancellationToken));
        Task<bool> UpdateAsync(T entity, CancellationToken ct = default(CancellationToken));
        Task<bool> DeleteAsync(int id, CancellationToken ct = default(CancellationToken), bool F = false);
        IEnumerable<IEntityType> getRelatedEntities();
        Task<DbSet<T>> IncludeSingleContext(DbSet<T> context, CancellationToken ct = default(CancellationToken));
        IQueryable<T> IncludeAllContext(IQueryable<T> q, CancellationToken ct);

    }
}

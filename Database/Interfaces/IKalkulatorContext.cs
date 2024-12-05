using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database.Interfaces
{
    public interface IKalkulatorContext : IDisposable
    {
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        DatabaseFacade Database { get; }

        EntityEntry<TEntity> Entry<TEntity>([NotNull] TEntity entity) where TEntity : class;

        Task AddRangeAsync([NotNull] params object[] entities);

        ValueTask<EntityEntry> AddAsync([NotNull] object entity, CancellationToken cancellationToken = default);

        EntityEntry Remove([NotNull] object entity);

        void RemoveRange([NotNull] IEnumerable<object> entities);

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

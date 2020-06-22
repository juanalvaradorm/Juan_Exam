using JMAR.SYSTEM.DOMAIN.Repositories;
using JMAR.SYSTEM.DOMAIN.Utils;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace JMAR.SYSTEM.DATA.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T> where T : class
    {

        private readonly DbContext _context;

        public BaseRepository(DbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }

        async Task<T> IBaseRepository<T>.AddAsync(T newEntity, CancellationToken ct)
        {
            if (HasSoftDelete())
            {
                var propertyInfoIsDeleted = newEntity.GetType().GetProperty("Active");
                propertyInfoIsDeleted.SetValue(newEntity, true, null);
            }

            _context.Set<T>().Add(newEntity);

            await _context.SaveChangesAsync(ct);

            return newEntity;
        }

        async Task<bool> IBaseRepository<T>.DeleteAsync(int id, CancellationToken ct, bool F)
        {
            var toRemove = _context.Set<T>().Find(id);

            ExtractAttributes<T> extract = new ExtractAttributes<T>();

            if (F == false && extract.ToString().Contains("ApplicationUser"))
            {
                _context.Set<T>().Remove(toRemove);
                await _context.SaveChangesAsync(ct);

                return true;
            }

            if (extract.ToString().Contains("ElementDesign") || extract.ToString().Contains("Skin"))
            {
                _context.Set<T>().Remove(toRemove);
                await _context.SaveChangesAsync(ct);

                return true;
            }


            if (HasSoftDelete())
            {
                PropertyInfo propertyInfoIsDeleted = toRemove.GetType().GetProperty("Active");
                PropertyInfo propertyInfoDeletedAt = toRemove.GetType().GetProperty("UpdatedAt");
                propertyInfoIsDeleted.SetValue(toRemove, false, null);
                propertyInfoDeletedAt.SetValue(toRemove, DateTime.Now, null);
                _context.Set<T>().Update(toRemove);
                await _context.SaveChangesAsync(ct);

                return true;
            }
            else
            {
                _context.Set<T>().Remove(toRemove);
                await _context.SaveChangesAsync(ct);

                return true;
            }
        }

        IQueryable<T> IBaseRepository<T>.GetAllAsync(CancellationToken ct)
        {

            ExtractAttributes<T> extract = new ExtractAttributes<T>();
            bool SiC = false;
            ICollection<string> ArrAtt = null;
            extract.ExtAttRelated(ref ArrAtt);


            if (ArrAtt.Count > 0)
            {

                var q = from b in _context.Set<T>() select b;
                return IncludeAllContext(q, ct).AsQueryable();

            }
            else
            {
                var q = from b in _context.Set<T>() select b;
                return q.AsQueryable();
            }

        }

        async Task<T> IBaseRepository<T>.GetByIdAsync(int? id, CancellationToken ct)
        {
            DbSet<T> context = _context.Set<T>();


            context = await IncludeSingleContext(context, ct);

            var result = await context.FindAsync(id);

            if (result == null)
                return result;

            bool isActive = true;

            if (HasSoftDelete())
            {
                var propertyInfoIsDeleted = result.GetType().GetProperty("Active");
                isActive = (bool)propertyInfoIsDeleted.GetValue(result);
            }

            return isActive ? result : result;
        }

        async Task<bool> IBaseRepository<T>.UpdateAsync(T entity, CancellationToken ct)
        {
            try
            {
                _context.Entry(entity).State = EntityState.Modified;
                _context.Set<T>().Update(entity);
                await _context.SaveChangesAsync(ct);
            }
            catch (Exception ex)
            {
                throw ex.InnerException;
            }
            return true;
        }

        private bool HasSoftDelete()
        {
            var interfaces = typeof(T).GetInterfaces();
            return interfaces.Any(x => x.Name.Contains("ISoftDelete"));
        }

        public IEnumerable<IEntityType> getRelatedEntities()
        {
            var entities = _context.Model.GetEntityTypes();

            return entities;
        }

        public virtual async Task<DbSet<T>> IncludeSingleContext(DbSet<T> context, CancellationToken ct)
        {
            await Task.Delay(1);
            return context;
        }

        public virtual IQueryable<T> IncludeAllContext(IQueryable<T> q, CancellationToken ct)
        {
            return q;
        }

        public virtual async Task<DbSet<T>> GetAllSored(DbSet<T> context, CancellationToken ct = default(CancellationToken))
        {
            await Task.Delay(1);
            return context;
        }

    }
}

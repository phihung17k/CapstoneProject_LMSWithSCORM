using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LMS.Infrastructure.Data
{
    public interface IBaseRepository<T, TKey> where T : class
    {
        Task<T> FindAsync(TKey id);
        IQueryable<T> GetAll();
        IQueryable<T> Get(Expression<Func<T, bool>> where);
        IQueryable<T> Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes);
        Task AddAsync(T entity);
        Task AddRange(IEnumerable<T> entities);
        void Update(T entity);
        Task<bool> Remove(TKey id);
        void RemoveRange(IEnumerable<T> entities);
    }
    public class BaseRepository<T, Tkey> : IBaseRepository<T, Tkey> where T : class
    {
        protected readonly ApplicationDbContext applicationDbContext;
        private DbSet<T> dbSet;
        public BaseRepository(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
            dbSet = this.applicationDbContext.Set<T>();
        }
        public virtual async Task AddAsync(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public virtual async Task AddRange(IEnumerable<T> entities)
        {
            await dbSet.AddRangeAsync(entities);
        }

        public virtual async Task<T> FindAsync(Tkey id)
        {
            return await dbSet.FindAsync(id);
        }

        public virtual IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> where)
        {
            return dbSet.Where(where);
        }

        public virtual IQueryable<T> Get(Expression<Func<T, bool>> where, params Expression<Func<T, object>>[] includes)
        {
            var result = dbSet.Where(where);
            foreach (var include in includes)
            {
                result = result.Include(include);
            }
            return result;
        }

        public virtual async Task<bool> Remove(Tkey id)
        {
            T entity = await dbSet.FindAsync(id);
            if (entity == null)
            {
                return false;
            }
            dbSet.Remove(entity);
            return true;
        }

        public virtual void RemoveRange(IEnumerable<T> entities)
        {
            dbSet.RemoveRange(entities);
        }

        public virtual void Update(T entity)
        {
            applicationDbContext.Entry<T>(entity).State = EntityState.Modified;
        }
    }
}

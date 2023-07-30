using DecisionTree.MVC.Infrastructure.DAL;
using DecisionTree.MVC.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DecisionTree.MVC.Infrastructure.Repositories
{
    public class BaseRepository<T, ID> : IRepository<T, ID> where T : class
    {
        protected readonly AppDbContext _ctx;
        protected readonly DbSet<T> _dbSet;

        public BaseRepository(AppDbContext ctx)
        {
            _ctx = ctx;
            _dbSet = ctx.Set<T>();
        }

        public virtual async Task<T> AddAsync(T entity)
        {
            await _dbSet.AddAsync(entity);
            return entity;
        }

        public virtual Task DeleteAsync(ID id)
        {
            T obj = _dbSet.Find(id);
            if (_ctx.Entry(obj).State == EntityState.Detached)
            {
                _dbSet.Attach(obj);
            }
            return DeleteAsync(obj);
        }

        public virtual Task DeleteAsync(T entity)
        {
            _dbSet.Remove(entity);
            return Task.CompletedTask;
        }

        public virtual Task UpdateAsync(T entity)
        {
            _dbSet.Update(entity);
            return Task.CompletedTask;
        }

        public virtual async Task<T> GetAsync(ID id)
        {
            return await _dbSet.FindAsync(id);
        }

        public virtual async Task<IEnumerable<T>> GetManyAsync(
            Expression<Func<T, bool>>? filter = null,
            Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null,
            int? pageSize = null,
            int? pageNumber = null)
        {
            IQueryable<T> query = _dbSet as IQueryable<T>;

            if (filter != null)
            {
                query = query.Where(filter);
            }

            //if(includedFields.Length > 0)
            //{
            //    query = includedFields.Aggregate(query, (theQuery, theInclude) => theQuery.Include(theInclude));
            //}

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (pageNumber.HasValue)
            {
                query = query.Skip(pageNumber.Value - 1);
            }
            if (pageSize.HasValue)
            {
                query = query.Take(pageSize.Value);
            }

            return await query.ToListAsync();
        }
    }
}

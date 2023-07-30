using System.Linq.Expressions;

namespace DecisionTree.MVC.Infrastructure.Repositories.Interfaces
{
    public interface IRepository<T, ID> where T : class
    {
        Task<T> AddAsync(T entity);
        Task UpdateAsync(T entity);
        Task DeleteAsync(ID id);
        Task<T> GetAsync(ID id);
        Task<IEnumerable<T>> GetManyAsync(
            Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            int? pageSize = null,
            int? pageNumber = null);
    }
}

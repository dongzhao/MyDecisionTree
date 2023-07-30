using DecisionTree.MVC.Core.Entities;
using DecisionTree.MVC.Infrastructure.DAL;
using DecisionTree.MVC.Infrastructure.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DecisionTree.MVC.Infrastructure.Repositories
{
    public class HierarchyItemRepository : BaseRepository<HierarchyItem, int>, IHierarchyItemRepository
    {
        public HierarchyItemRepository(AppDbContext ctx) : base(ctx)
        {

        }

        /// <summary>
        /// Delete current item and associated children
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int id)
        {
            var parent = await _ctx.HierarchyItemSet.Include(c => c.Children).SingleAsync(c => c.Id == id);
            await Remove(parent);
        }

        private Task Remove(HierarchyItem parent)
        {
            foreach(var child in parent.Children)
            {
                Remove(child);

                if (_ctx.Entry(parent).State == EntityState.Detached)
                {
                    _dbSet.Attach(parent);
                }
                _dbSet.Remove(parent);
            }

            if (_ctx.Entry(parent).State == EntityState.Detached)
            {
                _dbSet.Attach(parent);
            }
            _dbSet.Remove(parent);

            return Task.CompletedTask;
        }
    }
}

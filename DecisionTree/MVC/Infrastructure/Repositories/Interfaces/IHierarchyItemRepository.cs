using DecisionTree.MVC.Core.Entities;

namespace DecisionTree.MVC.Infrastructure.Repositories.Interfaces
{
    public interface IHierarchyItemRepository : IRepository<HierarchyItem, int>
    {
        /// <summary>
        /// Get all top hierarchyitem that has no any parent.
        /// </summary>
        /// <returns>Collection of hierarchyItem data</returns>
        Task<IEnumerable<HierarchyItem>> GetAllParent();

        Task<HierarchyItem> GetSingle(int id);
    }
}

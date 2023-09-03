using DecisionTree.Models.Documents;
using DecisionTree.MVC.Core.Entities;
using DecisionTree.MVC.Core.Enums;
using DecisionTree.MVC.Infrastructure;
using DecisionTree.MVC.Infrastructure.Repositories.Interfaces;

namespace DecisionTree.MVC.Application.Document
{
    /// <summary>
    /// A interface using proxy design pattern
    /// Document Service proxy
    /// </summary>
    public interface IDocumentService : IHierarchyItemRepository
    {
        Task<IEnumerable<HierarchyItem>> GetAllDocuments();
        Task<HierarchyItem> GetDocument(int id);
        Task SaveOrUpdate(HierarchyItem item);
    }
}

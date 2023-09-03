using DecisionTree.Models.Documents;
using DecisionTree.MVC.Core.Entities;
using DecisionTree.MVC.Core.Enums;
using DecisionTree.MVC.Infrastructure;
using DecisionTree.MVC.Infrastructure.Repositories;
using System.Xml.Linq;

namespace DecisionTree.MVC.Application.Document
{
    /// <summary>
    /// A implementation using proxy design pattern
    /// Document Service proxy
    /// </summary>
    public class DocumentService : HierarchyItemRepository, IDocumentService
    {
        public DocumentService(IUnitOfWork uow) : base(uow)
        {
        }

        public async Task<IEnumerable<HierarchyItem>> GetAllDocuments()
        {
            var documentTypes = new HierarchyType[] { HierarchyType.Folder, HierarchyType.Document };
            var items = await _uow.HierarchyItemRepository.GetManyAsync(null);
            return items.Where(c => documentTypes.Contains(c.ItemType));
        }

        public async Task<HierarchyItem> GetDocument(int id)
        {
            return await _uow.HierarchyItemRepository.GetSingle(id);
        }

        public async Task SaveOrUpdate(HierarchyItem item)
        {
            await _uow.HierarchyItemRepository.AddAsync(item);
            await _uow.CommitChangesAsync();
        }
    }
}

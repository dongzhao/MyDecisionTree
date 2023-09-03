using DecisionTree.MVC.Core.Enums;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DecisionTree.Models.Documents
{
    /// <summary>
    /// A document model view of db entity HierarchyItem 
    /// The object data was converted from populated HierarchyItem 
    /// via automapper 
    /// </summary>
    public class DocumentViewModel
    {
        public int Id { get; set; }
        public int? ParentId { get; set; }
        public string Title { get; set; } = string.Empty;
        public HierarchyType ItemType { get; set; }

        public List<SelectListItem> DocumentTypeSelection { 
            get 
            {
                return new List<SelectListItem> 
                { 
                    new SelectListItem{ Value = Convert.ToString((int)HierarchyType.Folder), Text = HierarchyType.Folder.ToString() }, 
                    new SelectListItem{ Value = Convert.ToString((int)HierarchyType.Document), Text = HierarchyType.Document.ToString() },
                }; 
            } 
        }
    }
}
